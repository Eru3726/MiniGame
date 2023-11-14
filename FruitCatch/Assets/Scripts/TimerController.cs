using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField, Header("ゲームタイム")]
    private float totalTime;

    [SerializeField, Header("タイムテキスト")]
    private Text timeText;

    private int seconds;
    private GameObject fruitGeneratorObj;
    private FruitGenerator fruitGenerator;
    private GameObject player;

    void Start()
    {
        fruitGeneratorObj = GameObject.Find("FruitGenerator");
        fruitGenerator = fruitGeneratorObj.GetComponent<FruitGenerator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!fruitGenerator.gameStart) return;
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
            Destroy(fruitGeneratorObj);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}