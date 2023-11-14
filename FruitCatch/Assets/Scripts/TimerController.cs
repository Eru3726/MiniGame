using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField, Header("ゲームタイム")]
    private float totalTime;

    [SerializeField, Header("タイムテキスト")]
    private Text timeText;

    private int seconds;
    private GameObject fruitGenerator;
    private GameObject player;

    void Start()
    {
        fruitGenerator = GameObject.Find("FruitGenerator");
        player = GameObject.Find("Player");
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
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().GameEnd();
            Destroy(fruitGenerator);
            Destroy(player);
        }
    }
}