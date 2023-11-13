using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("プレイヤー速度")]
    private float speed = 1.0f;

    [SerializeField, Header("右移動制限")]
    private float rightPositionBorder = 5.5f;

    [SerializeField, Header("左移動制限")]
    private float leftPositionBorder = -5.5f;

    private bool leftFlg = false;
    private bool rightFlg = false;

    void Update()
    {
        //←を入力時leftPositionBorderまで左移動
        if (leftFlg && this.transform.position.x > leftPositionBorder) this.transform.position += speed * Time.deltaTime * Vector3.left;

        //→を入力時rightPositionBorderまで右移動
        if (rightFlg && this.transform.position.x < rightPositionBorder) this.transform.position += speed * Time.deltaTime * Vector3.right;

#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.A) && this.transform.position.x > leftPositionBorder) this.transform.position += speed * Time.deltaTime * Vector3.left;
        if(Input.GetKey(KeyCode.D) && this.transform.position.x < rightPositionBorder) this.transform.position += speed * Time.deltaTime * Vector3.right;
#endif
    }

    public void OnLeftButtonDown()
    {
        leftFlg = true;
    }

    public void OnLeftButtonUp()
    {
        leftFlg = false;
    }

    public void OnRightButtonDown()
    {
        rightFlg = true;
    }

    public void OnRightButtonUp()
    {
        rightFlg = false;
    }
}
