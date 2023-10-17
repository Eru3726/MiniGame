using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    [SerializeField] Fade fade;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            In();
        }
    }

    //FadeIn
    public void In()
    {
        //1秒かけてFadeInする
        //FadeInだけしたいならこっち
        //fade.FadeIn(1f);

        //1秒かけてFadeInし終わったらScene2へ移行する
        //Scene移行もしたいならこっち
        fade.FadeIn(1f, () => SceneManager.LoadScene("Scene2"));
    }
}
