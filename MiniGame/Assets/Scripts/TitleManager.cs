using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Fade fade;
    public DataManager data;
    public Text text;
    public GameObject NameUI;
    public InputField inputField;

    [SerializeField]
    private GameObject rankingObj;

    [SerializeField]
    private GameObject canObj;

    [SerializeField]
    private PlayFabController playFab;

    private int characterLimit = 20;

    private bool nameFlg = false;

    private void Start()
    {
        NameUI.SetActive(false);
        rankingObj.SetActive(false);
        canObj.SetActive(false);
        data.Read();

        if (GameData.playerName == null)
        {
            NameUI.SetActive(true);
        }
        else
        {
            nameFlg = true;
            canObj.SetActive(true);
        }

        if (GameData.playerID == null)
        {
            GameData.playerID = (Random.Range(1, 10000) * Random.Range(1, 10000)).ToString();
            data.Save();
        }

        Debug.Log("PlayerID：" + GameData.playerID);
        Debug.Log("PlayerName：" + GameData.playerName);

        text.text = "PlayerID：" + GameData.playerID;

        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    public void Decision()
    {
        if (inputField != null)
        {
            GameData.playerName = inputField.text;

            data.Save();

            nameFlg = true;
            canObj.SetActive(true);

            NameUI.SetActive(false);
        }
    }


    private void OnInputValueChanged(string value)
    {
        if (value.Length > characterLimit)
        {
            // 文字数が制限を超えた場合は、制限以内にトリミング
            inputField.text = value.Substring(0, characterLimit);
        }
    }

    public void RankingOpen()
    {
        canObj.SetActive(false);
        rankingObj.SetActive(true);
        playFab.GetLeaderboard();
    }

    public void RankingClose()
    {
        rankingObj.SetActive(false);
        canObj.SetActive(true);
    }

    public void GamePlay()
    {
        Load.SL = 0;
        if (nameFlg) fade.FadeIn(1f, () => SceneManager.LoadScene("LoadScene"));
    }
}
