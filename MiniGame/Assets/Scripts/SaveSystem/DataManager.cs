//データをセーブしたり読み込んだりします

using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    //クラスの参照
    public Save saveClass;
    public Read readClass;

    //セーブファイル指定変数
    public static int saveFile = 1;

    //セーブデータがあるかの判定変数
    public static bool saveData = false;

    void Start()
    {
        Read();
    }

    public void Read()
    {
        //読み込む
        readClass.enabled = true;
        Debug.Log("読み込みがおわりました");
    }

    public void Save()
    {
        //セーブする
        saveClass.enabled = true;
        readClass.enabled = true;
        Debug.Log("セーブができました");
    }

    //セーブデータ削除
    public void Delete()
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
        File.Delete(path + "/save" + saveFile + ".bytes");

        //リロード
        readClass.enabled = true;

        Debug.Log("データの削除が終わりました");
    }

    //ファイル１
    public void File1()
    {
        saveFile = 1;
        Read();
    }

    //ファイル2
    public void File2()
    {
        saveFile = 2;
        Read();
    }

    //ファイル3
    public void File3()
    {
        saveFile = 3;
        Read();
    }
}