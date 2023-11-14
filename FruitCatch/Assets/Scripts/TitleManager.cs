using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        AudioPlay.instance.BGMPlay(0);
    }
    public void GameStart()
    {
        SceneManager.LoadScene("FruitCatch");
    }
}
