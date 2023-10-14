using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberPlateManager : MonoBehaviour
{
    private int[,,] defaultNum = new int[,,]
    {
        {
            {1,4,7,2,5,8,3,6,9},
            {2,5,8,3,6,9,4,7,1},
            {3,6,9,4,7,1,5,8,2},
            {4,7,1,5,8,2,6,9,3},
            {5,8,2,6,9,3,7,1,4},
            {6,9,3,7,1,4,8,2,5},
            {7,1,4,8,2,5,9,3,6},
            {8,2,5,9,3,6,1,4,7},
            {9,3,6,1,4,7,2,5,8}
        },
        {
            {7,2,6,3,4,8,1,9,5},
            {5,9,4,1,2,7,3,8,6},
            {3,1,8,6,5,9,7,4,2},
            {4,7,3,5,1,6,8,2,9},
            {6,8,2,9,7,3,4,5,1},
            {9,5,1,2,8,4,6,7,3},
            {1,4,5,7,6,2,9,3,8},
            {2,3,7,8,9,1,5,6,4},
            {8,6,9,4,3,5,2,1,7}
        },
        {
            {2,3,4,8,1,5,7,6,9},
            {5,8,1,9,7,6,4,2,3},
            {9,6,7,4,2,3,5,8,1},
            {3,4,2,6,8,1,9,5,7},
            {8,7,5,3,4,9,6,1,2},
            {6,1,9,2,5,7,3,4,8},
            {1,5,6,7,3,2,8,9,4},
            {4,2,3,5,9,8,1,7,6},
            {7,9,8,1,6,4,2,3,5}
        },
        {
            {2,9,7,8,4,5,1,6,3},
            {6,5,4,2,1,3,9,8,7},
            {8,3,1,7,6,9,2,5,4},
            {3,4,6,5,9,2,7,1,8},
            {9,2,8,6,7,1,3,4,5},
            {1,7,5,4,3,8,6,9,2},
            {7,6,3,1,8,4,5,2,9},
            {4,1,2,9,5,7,8,3,6},
            {5,8,9,3,2,6,4,7,1}
        },
        {
            {3,1,8,9,2,4,7,5,6},
            {7,4,9,6,8,5,3,2,1},
            {6,2,5,1,7,3,8,9,4},
            {5,9,7,8,6,2,4,1,3},
            {2,3,1,7,4,9,6,8,5},
            {8,6,4,3,5,1,9,7,2},
            {1,7,3,5,9,6,2,4,8},
            {9,5,2,4,3,8,1,6,7},
            {4,8,6,2,1,7,5,3,9}
        },
        {
            {1,5,7,9,6,4,2,3,8},
            {9,6,4,2,8,3,1,5,7},
            {3,2,8,5,7,1,4,9,6},
            {7,8,6,4,2,9,3,1,5},
            {4,3,9,1,5,8,6,7,2},
            {2,1,5,6,3,7,9,8,4},
            {5,4,3,7,9,2,8,6,1},
            {8,7,2,3,1,6,5,4,9},
            {6,9,1,8,4,5,7,2,3}
        },
        {
            {3,1,5,8,4,2,6,9,7},
            {7,9,4,5,6,1,3,8,2},
            {2,8,6,7,9,3,4,5,1},
            {6,3,9,1,2,8,7,4,5},
            {4,7,1,9,5,6,8,2,3},
            {8,5,2,4,3,7,9,1,6},
            {5,4,7,3,1,9,2,6,8},
            {9,2,3,6,8,5,1,7,4},
            {1,6,8,2,7,4,5,3,9}
        },
        {
            {1,9,7,8,6,3,4,2,5},
            {8,4,6,9,2,5,7,1,3},
            {5,3,2,1,4,7,8,6,9},
            {2,5,4,7,9,1,3,8,6},
            {3,6,8,2,5,4,1,9,7},
            {9,7,1,6,3,8,2,5,4},
            {4,8,3,5,1,9,6,7,2},
            {6,1,5,4,7,2,9,3,8},
            {7,2,9,3,8,6,5,4,1}
        },
        {
            {4,9,8,7,3,2,5,6,1},
            {6,1,2,9,5,4,7,8,3},
            {3,5,7,1,6,8,4,2,9},
            {2,3,5,6,1,9,8,7,4},
            {7,8,4,3,2,5,1,9,6},
            {9,6,1,8,4,7,2,3,5},
            {5,7,6,4,8,3,9,1,2},
            {8,4,3,2,9,1,6,5,7},
            {1,2,9,5,7,6,3,4,8}
        },
        {
            {4,2,8,5,6,3,1,7,9},
            {1,7,9,2,4,8,5,3,6},
            {3,5,6,1,7,9,2,4,8},
            {8,4,5,3,9,2,6,1,7},
            {6,1,7,4,8,5,9,2,3},
            {9,3,2,7,1,6,4,8,5},
            {7,9,4,6,3,1,8,5,2},
            {2,6,3,8,5,4,7,9,1},
            {5,8,1,9,2,7,3,6,4}
        },
    };

    private int[,] ansNum = new int[9, 9];

    private Text[,] text;

    private NumText[,] numTexts;

    private List<int> selectedNumbers = new List<int>();

    private int selectNum = 0;

    private NumText numText;

    [SerializeField, Header("空欄の数")]
    private int blankToSelect = 30;

    [SerializeField, Header("シャッフルする回数")]
    private int shuffle = 100;

    [SerializeField]
    private Text[] defaultText;

    [SerializeField]
    private NumText[] defaultNumText;

    private Image nowImage;

    void Start()
    {
        text = TextInit(defaultText);
        numTexts = NumTextInit(defaultNumText);
        AnsChange();
    }

    void Update()
    {
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
    }

    /// <summary>
    /// テンプレの初期化
    /// </summary>
    /// <returns></returns>
    private int[,] NumTempInit()
    {
        int[,] swappedArray = new int[9,9];
        int rand = Random.Range(0, 10);

        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                swappedArray[i, j] = defaultNum[rand, i, j];
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
        selectedNumbers.Clear();
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
        Debug.Log("ゲームクリア");
    }
}
