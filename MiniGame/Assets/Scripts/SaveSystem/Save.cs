//データをファイルに保存します

using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Save : MonoBehaviour
{
    void OnEnable()
    {
        DoSave();
    }

    private void DoSave()
    {
//#if UNITY_EDITOR
//        //UnityEditor上なら
//        //Assetファイルの中のSaveファイルのパスを入れる
//        string path = Application.dataPath + "/Save";

//#else
//        //そうでなければ
//        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
//        Directory.CreateDirectory("Save");
//        string path = Directory.GetCurrentDirectory() + "/Save";
        
//#endif

        string saveDirectory;

        if (Application.isEditor)
        {
            // エディター上で実行する場合、Assetsフォルダ内にセーブファイルを保存
            saveDirectory = Application.dataPath + "/Save";
        }
        else
        {
            // Androidデバイス上で実行する場合、外部ストレージにセーブファイルを保存
            saveDirectory = Path.Combine(Application.persistentDataPath, "Save");

            // ディレクトリが存在しない場合、作成
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
        }

        //セーブファイルのパスを設定
        string SaveFilePath = saveDirectory + "/save" + DataManager.saveFile + ".bytes";

        // セーブデータの作成
        SaveData saveData = CreateSaveData();

        // セーブデータをJSON形式の文字列に変換
        string jsonString = JsonUtility.ToJson(saveData);

        // 文字列をbyte配列に変換
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

        // AES暗号化
        byte[] arrEncrypted = AesEncrypt(bytes);

        // 指定したパスにファイルを作成
        FileStream file = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);

        //ファイルに保存する
        try
        {
            // ファイルに保存
            file.Write(arrEncrypted, 0, arrEncrypted.Length);

        }
        finally
        {
            // ファイルを閉じる
            if (file != null)
            {
                file.Close();
            }
        }
        this.enabled = false;//このスクリプトをオフにする
    }

    // セーブデータの作成
    private SaveData CreateSaveData()
    {
        //セーブデータのインスタンス化
        SaveData saveData = new SaveData();

        //ゲームデータの値をセーブデータに代入
        saveData.playerID = GameData.playerID;
        saveData.playerName = GameData.playerName;
        GameData.highScore = saveData.highScore;

        return saveData;
    }


    /// AesManagedマネージャーを取得

    private AesManaged GetAesManager()
    {
        //任意の半角英数16文字(Read.csと同じやつに)
        string aesIv = "3296823210648320";
        string aesKey = "8621374721350237";

        AesManaged aes = new AesManaged();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.IV = Encoding.UTF8.GetBytes(aesIv);
        aes.Key = Encoding.UTF8.GetBytes(aesKey);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }

    /// AES暗号化
    public byte[] AesEncrypt(byte[] byteText)
    {
        // AESマネージャーの取得
        AesManaged aes = GetAesManager();
        // 暗号化
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

}