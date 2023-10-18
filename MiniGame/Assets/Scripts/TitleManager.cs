using PlayFab;
using PlayFab.ClientModels;
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
            Debug.Log("PlayerName：" + GameData.playerName);
        }

        if (GameData.playerID == null)
        {
            GameData.playerID = (Random.Range(1, 10000) * Random.Range(1, 10000)).ToString();
            data.Save();
        }

        Debug.Log("PlayerID：" + GameData.playerID);

        text.text = "PlayerID：" + GameData.playerID;

        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    public void Decision()
    {
        if (inputField != null)
        {
            GameData.playerName = inputField.text;
            if (GameData.playerName == "") GameData.playerName = "NoName";
            Debug.Log("PlayerName：" + GameData.playerName);

            data.Save();

            nameFlg = true;
            canObj.SetActive(true);

            NameUI.SetActive(false);

            PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest
            {
                TitleId = PlayFabSettings.TitleId,
                CustomId = $"{GameData.playerID}",
                CreateAccount = true,
            }
        , result =>
        {
            Debug.Log("ログイン成功！");
            SetDisplayName(GameData.playerName);
            //SubmitScore(400);
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
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

    // アカウントの表示名を設定するメソッド
    public void SetDisplayName(string displayName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSetSuccess, OnDisplayNameSetFailure);
    }

    // 表示名設定成功時のコールバック
    private void OnDisplayNameSetSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display name set successfully!");
    }

    // 表示名設定失敗時のコールバック
    private void OnDisplayNameSetFailure(PlayFabError error)
    {
        Debug.LogError("Failed to set display name: " + error.ErrorMessage);
    }
}
