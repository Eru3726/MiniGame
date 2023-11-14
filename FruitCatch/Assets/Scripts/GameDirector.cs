using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class GameDirector : MonoBehaviour
{
    [SerializeField, Header("スコアテキスト")]
    private Text text;

    [SerializeField, Header("リザルトUI")]
    private GameObject resultUI;

    [SerializeField, Header("リザルトスコアテキスト")]
    private Text resultScoreText;

    [SerializeField, Header("リザルトハイスコアテキスト")]
    private Text resultHighScoreText;

    private int score = 0;

    private bool gameFlg = false;

    private int highScore;


    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        resultUI.SetActive(false);
        gameFlg = false;
    }

    public void FruitCount(int value)
    {
        if (gameFlg) return;
        score += value;
        if (score < 0) score = 0;
        
        text.text = string.Format("Score:{0:}", score);
    }

    public void GameEnd()
    {
        gameFlg = true;
        resultUI.SetActive(true);
        if (highScore < score) highScore = score;
        resultScoreText.text = "Score:" + score.ToString();
        resultHighScoreText.text = "HighScore:" + highScore.ToString();
    }

    private void OnDestroy()
    {
        Save();
    }

    // 上書き情報の保存
    public void Save()
    {
#if UNITY_EDITOR
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Application.dataPath + "/Save";

#elif UNITY_ANDROID
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Path.Combine(Application.persistentDataPath, "Save");

#endif

        //セーブファイルのパスを設定
        string SaveFilePath = path + "/save.bytes";

        // セーブデータの作成
        SkillTreeSaveData saveData = CreateSaveData();

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
    }

    public void Load()
    {
#if UNITY_EDITOR
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //セーブファイルのパスを設定
        string SaveFilePath = path + "/save.bytes";

        //セーブファイルがあるか
        if (File.Exists(SaveFilePath))
        {
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
                SkillTreeSaveData saveData = JsonUtility.FromJson<SkillTreeSaveData>(decryptStr);

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
            //初期化
            highScore = 0;
        }
    }

    // セーブデータの作成
    private SkillTreeSaveData CreateSaveData()
    {
        //セーブデータのインスタンス化
        SkillTreeSaveData saveData = new SkillTreeSaveData
        {
            highScore = highScore
        };

        return saveData;
    }

    //データの読み込み（反映）
    private void ReadData(SkillTreeSaveData saveData)
    {
        highScore = saveData.highScore;
    }

    /// <summary>
    ///  AesManagedマネージャーを取得
    /// </summary>
    /// <returns></returns>
    private AesManaged GetAesManager()
    {
        //任意の半角英数16文字
        string aesIv = "i9r032nu7f8g3wjk";
        string aesKey = "safi8902fniop2w4";

        AesManaged aes = new AesManaged
        {
            KeySize = 128,
            BlockSize = 128,
            Mode = CipherMode.CBC,
            IV = Encoding.UTF8.GetBytes(aesIv),
            Key = Encoding.UTF8.GetBytes(aesKey),
            Padding = PaddingMode.PKCS7
        };
        return aes;
    }

    /// <summary>
    /// AES暗号化
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesEncrypt(byte[] byteText)
    {
        // AESマネージャーの取得
        AesManaged aes = GetAesManager();
        // 暗号化
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary>
    /// AES復号化
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesDecrypt(byte[] byteText)
    {
        // AESマネージャー取得
        var aes = GetAesManager();
        // 復号化
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    //セーブデータ削除
    public void Init()
    {
#if UNITY_EDITOR
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //ファイル削除
        File.Delete(path + "/save.bytes");

        //リロード
        Load();

        Debug.Log("データの初期化が終わりました");
    }
}


[Serializable]
public class SkillTreeSaveData
{
    public int highScore;
}
