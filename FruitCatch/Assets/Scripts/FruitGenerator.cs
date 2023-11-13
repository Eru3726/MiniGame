using UnityEngine;

public class FruitGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject applePrefab;

    [SerializeField]
    private GameObject grapePrefab;

    [SerializeField]
    private GameObject peachPrefab;

    [SerializeField]
    private GameObject bomPrefab;

    public bool game = true;

    [SerializeField]
    private float rightBorder = 2.2f;

    [SerializeField]
    private float leftBorder = -2.2f;

    [SerializeField]
    private float startY = 2.6f;

    [SerializeField]
    private float appleRangeMin = 1.0f;

    [SerializeField]
    private float appleRangeMax = 3.0f;

    [SerializeField]
    private float grapeRangeMin = 1.0f;

    [SerializeField]
    private float grapeRangeMax = 3.0f;

    [SerializeField]
    private float peachRangeMin = 1.0f;

    [SerializeField]
    private float peachRangeMax = 3.0f;

    [SerializeField]
    private float bomRangeMin = 1.0f;

    [SerializeField]
    private float bomRangeMax = 3.0f;

    private float appleSpan;
    private float grapeSpan;
    private float peachSpan;
    private float bomSpan;

    private float appleDelta = 0;
    private float grapeDelta = 0;
    private float peachDelta = 0;
    private float bomDelta = 0;

    private void Start()
    {
        appleSpan = 0;
        grapeSpan = 1;
        peachSpan = 2;
        bomSpan = 3;
    }

    private void Update()
    {
        if (game == true)
        {
            Apple();
            Grape();
            Peach();
            Bom();
        }
    }

    private void Apple()
    {
        this.appleDelta += Time.deltaTime;
        if (this.appleDelta > this.appleSpan)
        {
            this.appleDelta = 0;
            GameObject go = Instantiate(applePrefab);
            float px = Random.Range(leftBorder, rightBorder);
            go.transform.position = new Vector3(px, startY, 0);
            appleSpan = Random.Range(appleRangeMin, appleRangeMax);
        }
    }

    private void Grape()
    {
        this.grapeDelta += Time.deltaTime;
        if (this.grapeDelta > this.grapeSpan)
        {
            this.grapeDelta = 0;
            GameObject go = Instantiate(grapePrefab);
            float px = Random.Range(leftBorder, rightBorder);
            go.transform.position = new Vector3(px, startY, 0);
            grapeSpan = Random.Range(grapeRangeMin, grapeRangeMax);
        }
    }

    private void Peach()
    {
        this.peachDelta += Time.deltaTime;
        if (this.peachDelta > this.peachSpan)
        {
            this.peachDelta = 0;
            GameObject go = Instantiate(peachPrefab);
            float px = Random.Range(leftBorder, rightBorder);
            go.transform.position = new Vector3(px, startY, 0);
            peachSpan = Random.Range(peachRangeMin, peachRangeMax);
        }
    }

    private void Bom()
    {
        this.bomDelta += Time.deltaTime;
        if (this.bomDelta > this.bomSpan)
        {
            this.bomDelta = 0;
            GameObject go = Instantiate(bomPrefab);
            float px = Random.Range(leftBorder, rightBorder);
            go.transform.position = new Vector3(px, startY, 0);
            bomSpan = Random.Range(bomRangeMin, bomRangeMax);
        }
    }
}
