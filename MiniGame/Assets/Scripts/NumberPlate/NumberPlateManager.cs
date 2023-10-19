using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberPlateManager : MonoBehaviour
{
    private readonly NumberTemplate numberTemplate = new();

    private int[,] ansNum = new int[9, 9];

    private Text[,] text;

    private NumText[,] numTexts;

    private readonly List<int> selectedNumbers = new();

    private NumText numText;

    [SerializeField, Header("空欄の数")]
    private int blankToSelect = 30;

    [SerializeField, Header("シャッフルする回数")]
    private int shuffle = 100;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text missText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text highScoreText;

    [SerializeField] 
    private Fade fade;

    [SerializeField]
    private GameObject clearUI;

    [SerializeField]
    private AdMobInterstitial interstitial;

    private float shakeDuration = 0;

    private float shakeAmount = 0.7f;

    private readonly float decreaseFactor = 1.0f;

    [SerializeField]
    private PlayFabController playFab;

    [SerializeField]
    private Text[] defaultText;

    [SerializeField]
    private NumText[] defaultNumText;

    [SerializeField]
    private Button[] numButton;

    private Image nowImage;

    private Transform cameraTransform;

    private Vector3 originalPos;

    private int missCount = 0;

    private float time = 0;

    private int minutes = 0;

    private int second = 0;

    private int timer = 0;

    private bool clearFlg = true;

    private int score = 0;

    void Start()
    {
        text = TextInit(defaultText);
        numTexts = NumTextInit(defaultNumText);
        cameraTransform = Camera.main.transform;
        originalPos = cameraTransform.localPosition;
        for (int i = 0; i < 9; i++) numButton[i].interactable = false;
        clearFlg = true;
        interstitial.number = GetComponent<NumberPlateManager>();
        AnsChange();
    }

    void Update()
    {
        if (clearFlg) return;

        //クリック処理
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<NumText>(out NumText numT))
            {
                if (nowImage != null) nowImage.color = Color.white;
                nowImage = numT.bg;
                nowImage.color = Color.green;
                numText = numT;

            }
        }

        //画面揺れ処理
        if (shakeDuration > 0)
        {
            cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            cameraTransform.localPosition = originalPos;
        }

        //タイム処理
        time += Time.deltaTime;
        if(time >= 1.0f)
        {
            time = 0;
            timer++;
            second++;
            if(second >= 60)
            {
                second = 0;
                minutes++;
            }
            timeText.text = minutes.ToString("d2") + ":" + second.ToString("d2");
        }
    }

    /// <summary>
    /// テンプレの初期化
    /// </summary>
    /// <returns></returns>
    private int[,] NumTempInit()
    {
        int[,] swappedArray = new int[9,9];
        int rand = Random.Range(0, numberTemplate.defaultNum.GetLength(0));

        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                swappedArray[i, j] = numberTemplate.defaultNum[rand, i, j];
            }
        }

        return swappedArray;
    }

    /// <summary>
    ///  空白にする場所をランダムで選択する
    /// </summary>
    private void SelectRandomNumbers()
    {
        for (int i = 0; i < blankToSelect; i++)
        {
            int randomNum;
            do
            {
                randomNum = Random.Range(0, 81);
            } while (selectedNumbers.Contains(randomNum));

            selectedNumbers.Add(randomNum);
        }
    }

    /// <summary>
    /// 解答をシャッフルする
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    private int[,] ShuffleAns(int[,] array)
    {
        int[,] swappedArray = (int[,])array.Clone();

        for (int i = 0; i < shuffle; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                //行
                if (Random.Range(0, 2) == 0)
                {
                    //3行
                    int place = Random.Range(0, 3);
                    if (place == 0)
                    {
                        //1～2入れ替え
                        swappedArray = SwapRows(swappedArray, 0, 3);
                        swappedArray = SwapRows(swappedArray, 1, 4);
                        swappedArray = SwapRows(swappedArray, 2, 5);
                    }
                    else if (place == 1)
                    {
                        //1～3入れ替え
                        swappedArray = SwapRows(swappedArray, 0, 6);
                        swappedArray = SwapRows(swappedArray, 1, 7);
                        swappedArray = SwapRows(swappedArray, 2, 8);
                    }
                    else
                    {
                        //2～3入れ替え
                        swappedArray = SwapRows(swappedArray, 3, 6);
                        swappedArray = SwapRows(swappedArray, 4, 7);
                        swappedArray = SwapRows(swappedArray, 5, 8);
                    }
                }
                else
                {
                    //1行
                    int rand1 = Random.Range(0, 3);
                    int rand2 = Random.Range(0, 3);
                    swappedArray = SwapRows(swappedArray, rand1, rand2);

                    rand1 = Random.Range(3, 6);
                    rand2 = Random.Range(3, 6);
                    swappedArray = SwapRows(swappedArray, rand1, rand2);

                    rand1 = Random.Range(6, 9);
                    rand2 = Random.Range(6, 9);
                    swappedArray = SwapRows(swappedArray, rand1, rand2);
                }
            }
            else
            {
                //列
                if (Random.Range(0, 2) == 0)
                {
                    //3列
                    int place = Random.Range(0, 3);
                    if (place == 0)
                    {
                        //1～2入れ替え
                        swappedArray = SwapColumns(swappedArray, 0, 3);
                        swappedArray = SwapColumns(swappedArray, 1, 4);
                        swappedArray = SwapColumns(swappedArray, 2, 5);
                    }
                    else if (place == 1)
                    {
                        //1～3入れ替え
                        swappedArray = SwapColumns(swappedArray, 0, 6);
                        swappedArray = SwapColumns(swappedArray, 1, 7);
                        swappedArray = SwapColumns(swappedArray, 2, 8);
                    }
                    else
                    {
                        //2～3入れ替え
                        swappedArray = SwapColumns(swappedArray, 3, 6);
                        swappedArray = SwapColumns(swappedArray, 4, 7);
                        swappedArray = SwapColumns(swappedArray, 5, 8);
                    }
                }
                else
                {
                    //1列
                    int rand1 = Random.Range(0, 3);
                    int rand2 = Random.Range(0, 3);
                    swappedArray = SwapColumns(swappedArray, rand1, rand2);

                    rand1 = Random.Range(3, 6);
                    rand2 = Random.Range(3, 6);
                    swappedArray = SwapColumns(swappedArray, rand1, rand2);

                    rand1 = Random.Range(6, 9);
                    rand2 = Random.Range(6, 9);
                    swappedArray = SwapColumns(swappedArray, rand1, rand2);
                }
            }
        }

        return swappedArray;
    }

    /// <summary>
    /// 行の入れ替え
    /// </summary>
    /// <param name="array"></param>
    /// <param name="row1"></param>
    /// <param name="row2"></param>
    /// <returns></returns>
    private int[,] SwapRows(int[,] array, int row1, int row2)
    {
        int[,] swappedArray = (int[,])array.Clone();

        for (int col = 0; col < array.GetLength(1); col++)
        {
            int temp = swappedArray[row1, col];
            swappedArray[row1, col] = swappedArray[row2, col];
            swappedArray[row2, col] = temp;
        }

        return swappedArray;
    }

    /// <summary>
    /// 列の入れ替え
    /// </summary>
    /// <param name="array"></param>
    /// <param name="col1"></param>
    /// <param name="col2"></param>
    /// <returns></returns>
    private int[,] SwapColumns(int[,] array, int col1, int col2)
    {
        int[,] swappedArray = (int[,])array.Clone();

        for (int row = 0; row < array.GetLength(0); row++)
        {
            int temp = swappedArray[row, col1];
            swappedArray[row, col1] = swappedArray[row, col2];
            swappedArray[row, col2] = temp;
        }

        return swappedArray;
    }

    /// <summary>
    /// テキスト表示
    /// </summary>
    public void DrawText()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                text[i, j].text = ansNum[i, j].ToString();
            }
        }
    }

    /// <summary>
    /// テキスト初期化
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    private Text[,] TextInit(Text[] array)
    {
        Text[,] texts = new Text[9, 9];
        int j = 0, k = 0;

        for (int i = 0; i < 81; i++)
        {
            texts[j, k] = array[i];
            k++;
            if ((i + 1) % 9 == 0 && i != 0)
            {
                k = 0;
                j++;
            }
        }

        return texts;
    }

    /// <summary>
    /// NumText初期化
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    private NumText[,] NumTextInit(NumText[] array)
    {
        NumText[,] numText = new NumText[9, 9];
        int j = 0, k = 0;

        for (int i = 0; i < 81; i++)
        {
            numText[j, k] = array[i];
            k++;
            if ((i + 1) % 9 == 0 && i != 0)
            {
                k = 0;
                j++;
            }
        }

        return numText;
    }

    /// <summary>
    /// NumTextの解答更新
    /// </summary>
    /// <param name="array"></param>
    private void NumTextAns(int[,] array)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                numTexts[i, j].ansInt = array[i, j];
                numTexts[i, j].ansFlg = true;
            }
        }
    }

    /// <summary>
    /// 問題変更
    /// </summary>
    public void AnsChange()
    {
        Init();
        ansNum = ShuffleAns(NumTempInit());
        NumTextAns(ansNum);
        DrawText();
        SelectRandomNumbers();
        HideText();
    }

    /// <summary>
    /// 解答を隠す
    /// </summary>
    public void HideText()
    {
        for (int i = 0; i < blankToSelect; i++)
        {
            int j = selectedNumbers[i] / 9;
            int k = selectedNumbers[i] % 9;
            text[j, k].text = "";
            numTexts[j, k].ansFlg = false;
        }
    }

    /// <summary>
    /// 数字入力ボタン
    /// </summary>
    /// <param name="value"></param>
    public void SelectNumButton(int value)
    {
        if (numText.ansFlg) return;
        numText.text.text = value.ToString();
        numText.text.color = Color.black;
        if (numText.ansInt != value) NumMiss();
        else
        {
            numText.ansFlg = true;
            if (AnsChack()) GameClear();
        }
    }

    /// <summary>
    /// 間違えた時の処理
    /// </summary>
    private void NumMiss()
    {
        numText.text.color = Color.red;
        ShakeCamera(0.1f, 0.2f);
        missCount++;
        missText.text = "Miss:" + missCount.ToString();
    }

    /// <summary>
    /// 解答確認
    /// </summary>
    /// <returns></returns>
    private bool AnsChack()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (!numTexts[i, j].ansFlg) return false;
            }
        }

        return true;
    }

    /// <summary>
    /// ゲームクリア
    /// </summary>
    private void GameClear()
    {
        clearFlg = true;
        interstitial.ShowAdMobInterstitial();
        score = (5000 - timer * 3) - (missCount * 50);
        for (int i = 0; i < 9; i++) numButton[i].interactable = false;
    }

    /// <summary>
    /// 画面揺れ
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="amount"></param>
    private void ShakeCamera(float duration, float amount)
    {
        shakeDuration = duration;
        shakeAmount = amount;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        missCount = 0;
        time = 0;
        timer = 0;
        minutes = 0;
        second = 0;
        score = 0;
        clearFlg = false;
        missText.text = "Miss:" + missCount.ToString();
        timeText.text = minutes.ToString("d2") + ":" + second.ToString("d2");
        selectedNumbers.Clear();
        for (int i = 0; i < 9; i++)
        {
            numButton[i].interactable = true;
            for(int j = 0; j < 9; j++)
            {
                text[i, j].color = Color.black;
            }
        }
        if (nowImage != null) nowImage.color = Color.white;
        nowImage = numTexts[0,0].bg;
        nowImage.color = Color.green;
        numText = numTexts[0, 0];
    }

    public void TitleBack()
    {
        Load.SL = 1;
        fade.FadeIn(1f, () => SceneManager.LoadScene("LoadScene"));
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // アプリがバックグラウンドに行ったとき
            // TitleSceneに戻る
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void Fin()
    {
        if (GameData.highScore < score) GameData.highScore = score;
        playFab.SubmitScore(score);
        clearUI.SetActive(true);
        scoreText.text = "スコア：" + score.ToString();
        highScoreText.text = "ハイスコア：" + GameData.highScore.ToString();
    }
}
