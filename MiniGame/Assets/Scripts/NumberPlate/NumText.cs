using UnityEngine;
using UnityEngine.UI;

public class NumText : MonoBehaviour
{
    public Image bg;
    public Text text;
    public int ansInt;

    void Start()
    {
        bg = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        bg.color = Color.white;
    }

    void Update()
    {
        
    }
}
