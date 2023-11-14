using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

public class GameDirector : MonoBehaviour
{
    [SerializeField, Header("カウントテキスト")]
    private Text countText;

    [SerializeField, Header("スコアテキスト")]
    private Text scoreText;

    [SerializeField, Header("リザルトUI")]
    private GameObject resultUI;

    [SerializeField, Header("リザルトスコアテキスト")]
    private Text resultScoreText;

    [SerializeField, Header("リザルトハイスコアテキスト")]
    private Text resultHighScoreText;

    private int score = 0;

    private bool gameFlg = false;

    private int highScore;

    private FruitGenerator fruitGenerator;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        resultUI.SetActive(false);
        gameFlg = false;
        GameObject fruitGeneratorObj = GameObject.Find("FruitGenerator");
        fruitGenerator = fruitGeneratorObj.GetComponent<FruitGenerator>();
        fruitGenerator.gameStart = false;
        StartCoroutine(StartCount());
    }

    public void FruitCount(int value)
    {
        if (gameFlg) return;
        score += value;
        if (score < 0) score = 0;

        scoreText.text = string.Format("Score:{0:}", score);
    }

    public void GameEnd()
    {
        if (gameFlg) return;
        gameFlg = true;
        AudioPlay.instance.BGMStop();
        AudioPlay.instance.SEPlay(1);
        resultUI.SetActive(true);
        if (highScore < score) highScore = score;
        resultScoreText.text = "Score:" + score.ToString();
        resultHighScoreText.text = "HighScore:" + highScore.ToString();
    }

    public void ReturnToTitle()
    {

        SceneManager.LoadScene("Title");
    }

    private IEnumerator StartCount()
    {
        AudioPlay.instance.BGMStop();
        countText.enabled = true;
        countText.text = "フルーツを\nキャッチして！";
        yield return new WaitForSeconds(1.0f);
        AudioPlay.instance.SEPlay(0);
        Debug.Log("SEEnd2");
        countText.text = "3";
        yield return new WaitForSeconds(0.75f);
        countText.text = "2";
        yield return new WaitForSeconds(0.75f);
        countText.text = "1";
        yield return new WaitForSeconds(0.75f);
        countText.text = "スタート";
        AudioPlay.instance.BGMPlay(0);
        fruitGenerator.gameStart = true;
        yield return new WaitForSeconds(0.75f);
        countText.enabled = false;
        yield break;
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
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        string path = Application.persistentDataPath;
        
#endif

        //セーブファイルのパスを設定
        string SaveFilePath = path + "/save.bytes";

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
        string path = Application.persistentDataPath;
        
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
            //初期化
            highScore = 0;
        }
    }

    // セーブデータの作成
    private SaveData CreateSaveData()
    {
        //セーブデータのインスタンス化
        SaveData saveData = new SaveData
        {
            highScore = highScore
        };

        return saveData;
    }

    //データの読み込み（反映）
    private void ReadData(SaveData saveData)
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
        string aesIv = "safi8902fniop2w4";
        string aesKey = "dsag75e7t32w7fd8";

        AesManaged aes = new()
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

        //ファイル削除
        File.Delete(saveDirectory + "/save.bytes");

        //リロード
        Load();

        Debug.Log("データの初期化が終わりました");
    }
}


[Serializable]
public class SaveData
{
    public int highScore;
}
