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

    private int characterLimit = 20;

    private bool nameFlg = false;

    private void Start()
    {
        NameUI.SetActive(false);

        data.Read();

        if (GameData.playerName == null)
        {
            NameUI.SetActive(true);
        }
        else
        {
            nameFlg = true;
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

    void Update()
    {
        if (Input.anyKey && nameFlg)
        {
            fade.FadeIn(1f, () => SceneManager.LoadScene("LoadScene"));
        }
    }

    public void Decision()
    {
        if (inputField != null)
        {
            GameData.playerName = inputField.text;

            data.Save();

            nameFlg = true;

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
}
