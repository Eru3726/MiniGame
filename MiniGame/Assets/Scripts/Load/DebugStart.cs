using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugStart : MonoBehaviour
{
    [SerializeField] Fade fade;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //左クリックしたら1秒かけてFadeInし、LoadSceneに移行する
            fade.FadeIn(1f, () => SceneManager.LoadScene("LoadScene"));
        }
    }
}
