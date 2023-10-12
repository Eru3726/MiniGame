using UnityEngine;
using UnityEngine.UI;

public class NumberPlateManager : MonoBehaviour
{
    private int[,] defaultNum = new int[,]
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
    };

    private int[,] ansNum = new int[9, 9];

    private Text[,] text;

    [SerializeField]
    private int shuffle = 100;

    [SerializeField]
    private Text[] defaultText;

    void Start()
    {
        text = TextInit(defaultText);
        AnsChange();
    }
    
    void Update()
    {

    }

    private int[,] ShuffleAns(int[,] array)
    {
        int[,] swappedArray = (int[,])array.Clone();

        for (int i = 0;i< shuffle; i++)
        {
            if(Random.Range(0,2) == 0)
            {
                //行
                if(Random.Range(0, 2) == 0)
                {
                    //3行
                    int place = Random.Range(0, 3);
                    if(place == 0)
                    {
                        //1～2入れ替え
                        swappedArray = SwapRows(swappedArray, 0, 3);
                        swappedArray = SwapRows(swappedArray, 1, 4);
                        swappedArray = SwapRows(swappedArray, 2, 5);
                    }
                    else if(place == 1)
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
    private void DrawText()
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

        for(int i = 0; i < 81; i++)
        {
            texts[j, k] = array[i];
            k++;
            if((i + 1) % 9 == 0 && i != 0)
            {
                k = 0;
                j++;
            }
        }

        return texts;
    }

    /// <summary>
    /// 問題変更
    /// </summary>
    public void AnsChange()
    {
        ansNum = ShuffleAns(defaultNum);
        DrawText();
    }
}
