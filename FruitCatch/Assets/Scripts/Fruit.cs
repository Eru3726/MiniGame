using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField]
    private float speed =0.1f;

    [SerializeField]
    private int score;

    private float border = -6.0f;

    void Update()
    {
        //フレームごとに等速で落下させる
        transform.Translate(0, -speed * Time.deltaTime, 0);

        //画面買いに出たらオブジェクトを破壊する
        if (transform.position.y < border)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //監督スクリプトにプレイヤと衝突したことを伝える
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().FruitCount(score);

            //衝突した場合は矢を消す
            Destroy(gameObject);
        }
    }
}
