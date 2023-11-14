using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject muteImage;

    private bool muteFlg = false;

    private void Start()
    {
        AudioPlay.instance.BGMPlay(0);
        muteFlg = AudioPlay.instance.bgmMute;
        muteImage.SetActive(muteFlg);
    }
    public void GameStart()
    {
        SceneManager.LoadScene("FruitCatch");
    }

    public void MuteButton()
    {
        muteFlg = !muteFlg;
        muteImage.SetActive(muteFlg);
        AudioPlay.instance.bgmMute = muteFlg;
        AudioPlay.instance.seMute = muteFlg;
    }
}
