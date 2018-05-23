using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMainPlayer1 : MonoBehaviour
{
    public Text GameOverText;
    public Text YourScorePlayer1;
    public GameObject GameOverPanel;
    public GameObject GamePanel;
    private int ScorePlayer1Point;

    private TilePlayer1[,] TilesPlayer1 = new TilePlayer1[4, 4];
    Stack<int> previousTilesPlayer1 = new Stack<int>();

    void Start()
    {

        //This line returns the numbers of TilesPlayer1 in the Scene which is on the UI (Total:16)
        ScorePlayer1.Instance.ScoreUpdate = 0;
        ScorePlayer1.Instance.TurnUpdate = 0;
        GameOverPanel.SetActive(false);
        GamePanel.SetActive(true);
        TilePlayer1[] AllTilesPlayer1AtOnce = GameObject.FindObjectsOfType<TilePlayer1>(); //this method is used to get all the TilesPlayer1 in our scene when it starts. But this array will be gone out of this method Start()
        for (int i = 0; i < AllTilesPlayer1AtOnce.Length; i++)
        {
            AllTilesPlayer1AtOnce[i].Appear = 0;
            TilesPlayer1[AllTilesPlayer1AtOnce[i].Row, AllTilesPlayer1AtOnce[i].Column] = AllTilesPlayer1AtOnce[i];

        }
        Generate();
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        Start();
    }


    public void preTilesPlayer1()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                previousTilesPlayer1.Push(TilesPlayer1[i, j].Appear);

            }
        }

    }

    public void Undo()
    {
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                TilesPlayer1[i, j].Appear = previousTilesPlayer1.Pop();
            }
        }

    }
    void Generate()
    {
        int i = UnityEngine.Random.Range(0, 4);
        int j = UnityEngine.Random.Range(0, 4);


        if (TilesPlayer1[i, j].Appear == 0)
        {
            TilesPlayer1[i, j].Appear = UnityEngine.Random.Range(1, 3);
        }
        else if (TilesPlayer1[i, j].Appear != 0)
        {
            Generate();
        }

    }
    private void GameOver()
    {

        GamePanel.SetActive(false);
        YourScorePlayer1.text = ScorePlayer1.Instance.ScoreUpdate.ToString();
        GameOverPanel.SetActive(true);
    }



    void MoveToLeft(TilePlayer1[,] a)
    {
        int TurnLimit = 100;
        int n = 2;
        while (n != 0)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j > 0; j--)
                {
                    //swap
                    if (a[i, j].Appear != 0 && a[i, j - 1].Appear == 0)
                    {
                        a[i, j - 1].Appear = a[i, j].Appear;
                        a[i, j].Appear = 0;
                    }

                    //merge and ScorePlayer1
                    if (a[i, j].Appear == a[i, j - 1].Appear && a[i, j - 1].Appear != 0 && a[i, j].Appear != 0)
                    {
                        a[i, j - 1].Appear = a[i, j - 1].Appear + 1;
                        a[i, j].Appear = 0;
                        ScorePlayer1Point = (int)Math.Pow(2, a[i, j - 1].Appear);
                        ScorePlayer1.Instance.ScoreUpdate += ScorePlayer1Point;
                    }
                    if (a[i, j].Appear != a[i, j - 1].Appear && a[i, j].Appear != 0 && a[i, j - 1].Appear != 0)
                    {
                        TurnLimit += 1;
                    }

                }
            }
            n--;
        }
        ScorePlayer1.Instance.TurnUpdate += 1;
        if (TurnLimit == 24)//Can/t move there
        {
            ScorePlayer1.Instance.TurnUpdate -= 1;
        }

        n = 2;
        int GOCount = 0;
        while (n != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j > 0; j--)
                {
                    if (a[i, j].Appear != 0)
                    {
                        if (a[i, j].Appear != a[i + 1, j].Appear && a[i, j].Appear != a[i, j - 1].Appear &&
                            a[i, j].Appear != 0 && a[i + 1, j].Appear != 0 && a[i, j - 1].Appear != 0
                            )
                        {
                            GOCount += 1;
                        }
                    }

                }
            }
            n--;
        }




        /* if (ScorePlayer1.Instance.TurnUpdate == 0)
         {
             GameOver();

         }
         else*/
        if (GOCount == (18))
        {
            for (int k = 0; k < 6; k++)
            {
                if (a[0, 0].Appear != a[1, 0].Appear && a[1, 0].Appear != a[2, 0].Appear && a[2, 0].Appear != a[3, 0].Appear &&
                    a[3, 3].Appear != a[3, 2].Appear && a[3, 2].Appear != a[3, 1].Appear && a[3, 1].Appear != a[3, 0].Appear &&
                    a[0, 0].Appear != 0 && a[1, 0].Appear != 0 && a[2, 0].Appear != 0 && a[3, 0].Appear != 0 &&
                    a[3, 1].Appear != 0 && a[3, 2].Appear != 0 && a[3, 3].Appear != 0
                    )
                {
                    GameOver();
                    break;
                }

            }

        }

    }

    /* void MoveToLeft(TilePlayer1[,] a)
     {
         int TurnLimit = 0;
         int n = 2;
         while (n != 0)
         {
             for (int i = 0; i < 4; i++)
             {
                 for (int j = 3; j > 0; j--)
                 {
                     //swap
                     if (a[i, j].Appear != 0 && a[i, j - 1].Appear == 0)
                     {
                         a[i, j - 1].Appear = a[i, j].Appear;
                         a[i, j].Appear = 0;
                     }

                     //merge and ScorePlayer1
                     if (a[i, j].Appear == a[i, j - 1].Appear && a[i, j - 1].Appear != 0 && a[i, j].Appear != 0)
                     {
                         a[i, j - 1].Appear = a[i, j - 1].Appear + 1;
                         a[i, j].Appear = 0;
                         ScorePlayer1Point = (int)Math.Pow(2, a[i, j - 1].Appear);
                         ScorePlayer1.Instance.ScoreUpdate += ScorePlayer1Point;
                     }
                     if (a[i, j].Appear != a[i, j - 1].Appear && a[i, j].Appear != 0 && a[i, j - 1].Appear != 0)
                     {
                         TurnLimit += 1;
                     }

                 }
             }
             n--;
         }
         ScorePlayer1.Instance.TurnUpdate -= 1;
         if (TurnLimit == 24)//Can/t move there
         {
             ScorePlayer1.Instance.TurnUpdate -= 1;
         }

         n = 2;
         int GOCount = 0;
         while (n != 0)
         {
             for (int i = 0; i < 3; i++)
             {
                 for (int j = 3; j > 0; j--)
                 {
                     if (a[i, j].Appear != 0)
                     {
                         if (a[i, j].Appear != a[i + 1, j].Appear && a[i, j].Appear != a[i, j - 1].Appear &&
                             a[i, j].Appear != 0 && a[i + 1, j].Appear != 0 && a[i, j - 1].Appear != 0
                             )
                         {
                             GOCount += 1;
                         }
                     }

                 }
             }
             n--;
         }



         if (ScorePlayer1.Instance.TurnUpdate == 0)
         {
             GameOver();

         }
        else if (GOCount == (18) )
         {
             for (int k = 0; k < 6; k++)
             {
                 if (a[0, 0].Appear != a[1, 0].Appear && a[1, 0].Appear != a[2, 0].Appear && a[2, 0].Appear != a[3, 0].Appear && 
                     a[3, 3].Appear != a[3, 2].Appear && a[3, 2].Appear != a[3, 1].Appear && a[3, 1].Appear != a[3, 0].Appear 
                     )
                 {
                     GameOver();
                     break;
                 }

             }

         }

     }*/

    void RotateLeft()
    {
        TilePlayer1[,] temp = new TilePlayer1[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                temp[i, j] = TilesPlayer1[i, j];

            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                TilesPlayer1[i, j] = temp[j, 4 - i - 1];
            }
        }

    }

    void RotateRight()
    {
        TilePlayer1[,] temp = new TilePlayer1[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {

                temp[i, j] = TilesPlayer1[i, j];

            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                TilesPlayer1[i, j] = temp[4 - j - 1, i];

            }
        }
    }

    void MoveLeft()
    {
        MoveToLeft(TilesPlayer1);
    }

    void MoveRight()
    {
        RotateLeft();
        RotateLeft();
        MoveToLeft(TilesPlayer1);
        RotateRight();
        RotateRight();

    }

    void MoveUp()
    {
        RotateLeft();
        MoveToLeft(TilesPlayer1);
        RotateRight();

    }

    void MoveDown()
    {
        RotateRight();
        MoveToLeft(TilesPlayer1);
        RotateLeft();
    }

    public void ReturnToMenuAction()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Move(DirectionPlayer1 d)
    {
        preTilesPlayer1();
        Debug.Log(d.ToString() + " move.");
        switch (d)
        {
            case DirectionPlayer1.Up:
                MoveUp();
                break;
            case DirectionPlayer1.Down:
                MoveDown();
                break;
            case DirectionPlayer1.Right:
                MoveRight();
                break;
            case DirectionPlayer1.Left:
                MoveLeft();
                break;
        }

        Generate();
    }


}
