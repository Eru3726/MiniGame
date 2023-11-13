using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField, Header("�Q�[���^�C��")]
    private float totalTime;

    [SerializeField, Header("�^�C���e�L�X�g")]
    private Text timeText;

    [SerializeField, Header("���U���gUI")]
    private GameObject resultUI;

    private int seconds;
    private GameObject fruitGenerator;

    void Start()
    {
        fruitGenerator = GameObject.Find("FruitGenerator");
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
            resultUI.SetActive(true);
        }
    }
}