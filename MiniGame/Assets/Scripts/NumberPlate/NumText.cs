using UnityEngine;
using UnityEngine.UI;

public class NumText : MonoBehaviour
{
    public Image bg;
    public Text text;
    public int ansInt;
    public bool ansFlg = false;

    void Start()
    {
        bg = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        bg.color = Color.white;
        ansFlg = true;
    }

    void Update()
    {
        
    }
}
