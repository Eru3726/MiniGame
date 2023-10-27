using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayFabController : MonoBehaviour
{
    public Text[] text;

    void Start()
    {
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
            Debug.Log("PlayerName：" + GameData.playerName);
            SetDisplayName(GameData.playerName);
            //SubmitScore(400);
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });

    }

    public void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = playerScore
                }
            }
        }, result =>
        {
            Debug.Log($"スコア {playerScore} 送信完了！");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void GetLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "HighScore"
        }, result =>
        {
            foreach (var item in result.Leaderboard)
            {
                Debug.Log($"{item.Position + 1}位:{item.DisplayName} " + $"スコア {item.StatValue}");

                text[item.Position].text = $"{item.Position + 1}位:{item.DisplayName} " + $"Score:{item.StatValue}";

                if (item.Position + 1 == 10)
                {
                    break;
                }
            }
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
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