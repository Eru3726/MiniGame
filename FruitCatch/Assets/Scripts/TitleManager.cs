using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Text tapText;

    [SerializeField]
    private float speed = 1.0f;

    private float alpha = 1;
    private bool flg = true;

    private void Start()
    {
        alpha = 1;
        flg = true;
    }

    void Update()
    {
        if (flg) alpha -= speed * Time.deltaTime;
        else alpha += speed * Time.deltaTime;

        if (alpha < 0) flg = false;
        else if(alpha > 1) flg = true;

        tapText.color = new Color(tapText.color.r, tapText.color.g, tapText.color.b, alpha);

        if (Input.anyKey) SceneManager.LoadScene("FruitCatch");
    }
}
