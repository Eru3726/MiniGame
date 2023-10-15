using UnityEngine;
using UnityEngine.UI;

public class NumText : MonoBehaviour
{
    [HideInInspector]
    public Image bg;

    [HideInInspector]
    public Text text;

    [HideInInspector]
    public int ansInt;

    [HideInInspector]
    public bool ansFlg = false;

    void Start()
    {
        bg = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        bg.color = Color.white;
        ansFlg = true;
    }
}
