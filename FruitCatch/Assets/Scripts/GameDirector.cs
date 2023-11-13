using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField, Header("スコアテキスト")]
    private Text text;

    private int count = 0;

    public void fruitCount(int value)
    {
        count += value;
        if (count < 0) count = 0;
        
        text.text = string.Format("Score:{0:}", count);
    }
}
