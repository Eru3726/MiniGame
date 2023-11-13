using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField, Header("ゲームタイム")]
    private float totalTime;

    [SerializeField, Header("タイムテキスト")]
    private Text timeText;

    [SerializeField, Header("リザルトUI")]
    private GameObject resultUI;

    private int seconds;
    private GameObject fruitGenerator;
    private GameObject player;

    void Start()
    {
        fruitGenerator = GameObject.Find("FruitGenerator");
        player = GameObject.Find("Player");
        resultUI.SetActive(false);
    }

    void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            seconds = (int)totalTime;
            timeText.text = seconds.ToString() + "s";
        }
        else
        {
            Destroy(fruitGenerator);
            Destroy(player);
            resultUI.SetActive(true);
        }
    }
}