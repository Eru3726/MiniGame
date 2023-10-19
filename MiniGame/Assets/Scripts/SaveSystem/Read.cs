//ファイルのデータを読み込みます

using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Read : MonoBehaviour
{
    void OnEnable()
    {
        DoRead();
    }

    private void DoRead()
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

        //セーブファイルがあるか
        if (File.Exists(SaveFilePath))
        {
            DataManager.saveData = true;

            //ファイルモードをオープンにする
            FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);
            try
            {
                // ファイル読み込み
                byte[] arrRead = File.ReadAllBytes(SaveFilePath);

                // 復号化
                byte[] arrDecrypt = AesDecrypt(arrRead);

                // byte配列を文字列に変換
                string decryptStr = Encoding.UTF8.GetString(arrDecrypt);

                // JSON形式の文字列をセーブデータのクラスに変換
                SaveData saveData = JsonUtility.FromJson<SaveData>(decryptStr);

                //データの反映
                ReadData(saveData);

            }
            finally
            {
                // ファイルを閉じる
                if (file != null)
                {
                    file.Close();
                }
            }
        }
        else
        {
            Debug.Log("セーブファイルがありません");
            DataManager.saveData = false;
        }

        this.enabled = false;

    }

    //データの読み込み（反映）
    private void ReadData(SaveData saveData)
    {
        GameData.playerID = saveData.playerID;
        GameData.playerName = saveData.playerName;
        GameData.highScore = saveData.highScore;
    }


    /// AesManagedマネージャーを取得
    private AesManaged GetAesManager()
    {
        //任意の半角英数16文字(Save.csと同じやつに)
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

    /// AES復号化
    public byte[] AesDecrypt(byte[] byteText)
    {
        // AESマネージャー取得
        var aes = GetAesManager();
        // 復号化
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

}