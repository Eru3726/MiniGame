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

    private Rigidbody2D rb;

    private FruitGenerator fruitGenerator;

    private void Start()
    {
        GameObject fruitGeneratorObj = GameObject.Find("FruitGenerator");
        fruitGenerator = fruitGeneratorObj.GetComponent<FruitGenerator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!fruitGenerator.gameStart) return;

        //←を入力時leftPositionBorderまで左移動
        if (leftFlg)
        {
            if (this.transform.position.x < leftPositionBorder) rb.velocity = Vector2.zero;
            else rb.velocity = speed * Vector3.left;
        }

        //→を入力時rightPositionBorderまで右移動
        if (rightFlg)
        {
            if (this.transform.position.x > rightPositionBorder) rb.velocity = Vector2.zero;
            else rb.velocity = speed * Vector3.right;
        }

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
        {
            if (this.transform.position.x < leftPositionBorder) rb.velocity = Vector2.zero;
            else rb.velocity = speed * Vector3.left;
        }
        else if(Input.GetKeyUp(KeyCode.A)) rb.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            if (this.transform.position.x > rightPositionBorder) rb.velocity = Vector2.zero;
            else rb.velocity = speed * Vector3.right;
        }
        else if (Input.GetKeyUp(KeyCode.D)) rb.velocity = Vector2.zero;
#endif
    }

    public void OnLeftButtonDown()
    {
        leftFlg = true;
    }

    public void OnLeftButtonUp()
    {
        leftFlg = false;
        rb.velocity = Vector2.zero;
    }

    public void OnRightButtonDown()
    {
        rightFlg = true;
    }

    public void OnRightButtonUp()
    {
        rightFlg = false;
        rb.velocity = Vector2.zero;
    }
}
