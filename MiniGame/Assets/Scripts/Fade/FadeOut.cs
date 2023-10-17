using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] Fade fade;
    void Start()
    {
        fade.FadeOut(1f);
    }

}
