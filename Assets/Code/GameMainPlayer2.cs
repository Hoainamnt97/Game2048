using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMainPlayer2 : MonoBehaviour
{
    public Text GameOverText;
    public Text YourScorePlayer2;
    public GameObject GameOverPanel;
    public GameObject GamePanel;
    private int ScorePlayer2Point;

    private TilePlayer2[,] TilesPlayer2 = new TilePlayer2[4, 4];
    Stack<int> previousTilesPlayer2 = new Stack<int>();

    void Start()
    {

        //This line returns the numbers of TilesPlayer2 in the Scene which is on the UI (Total:16)
        ScorePlayer2.Instance.ScoreUpdate = 0;
        ScorePlayer2.Instance.TurnUpdate = 0;
        GameOverPanel.SetActive(false);
        GamePanel.SetActive(true);
        TilePlayer2[] AllTilesPlayer2AtOnce = GameObject.FindObjectsOfType<TilePlayer2>(); //this method is used to get all the TilesPlayer2 in our scene when it starts. But this array will be gone out of this method Start()
        for (int i = 0; i < AllTilesPlayer2AtOnce.Length; i++)
        {
            AllTilesPlayer2AtOnce[i].Appear = 0;
            TilesPlayer2[AllTilesPlayer2AtOnce[i].Row, AllTilesPlayer2AtOnce[i].Column] = AllTilesPlayer2AtOnce[i];

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


    public void preTilesPlayer2()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                previousTilesPlayer2.Push(TilesPlayer2[i, j].Appear);

            }
        }

    }

    public void Undo()
    {
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                TilesPlayer2[i, j].Appear = previousTilesPlayer2.Pop();
            }
        }

    }
    void Generate()
    {
        int i = UnityEngine.Random.Range(0, 4);
        int j = UnityEngine.Random.Range(0, 4);


        if (TilesPlayer2[i, j].Appear == 0)
        {
            TilesPlayer2[i, j].Appear = UnityEngine.Random.Range(1, 3);
        }
        else if (TilesPlayer2[i, j].Appear != 0)
        {
            Generate();
        }

    }
    private void GameOver()
    {

        GamePanel.SetActive(false);
        YourScorePlayer2.text = ScorePlayer2.Instance.ScoreUpdate.ToString();
        GameOverPanel.SetActive(true);
    }



    void MoveToLeft(TilePlayer2[,] a)
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

                    //merge and ScorePlayer2
                    if (a[i, j].Appear == a[i, j - 1].Appear && a[i, j - 1].Appear != 0 && a[i, j].Appear != 0)
                    {
                        a[i, j - 1].Appear = a[i, j - 1].Appear + 1;
                        a[i, j].Appear = 0;
                        ScorePlayer2Point = (int)Math.Pow(2, a[i, j - 1].Appear);
                        ScorePlayer2.Instance.ScoreUpdate += ScorePlayer2Point;
                    }
                    if (a[i, j].Appear != a[i, j - 1].Appear && a[i, j].Appear != 0 && a[i, j - 1].Appear != 0)
                    {
                        TurnLimit += 1;
                    }

                }
            }
            n--;
        }
        ScorePlayer2.Instance.TurnUpdate += 1;
        if (TurnLimit == 24)//Can/t move there
        {
            ScorePlayer2.Instance.TurnUpdate -= 1;
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




        /* if (ScorePlayer2.Instance.TurnUpdate == 0)
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

    /* void MoveToLeft(TilePlayer2[,] a)
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

                     //merge and ScorePlayer2
                     if (a[i, j].Appear == a[i, j - 1].Appear && a[i, j - 1].Appear != 0 && a[i, j].Appear != 0)
                     {
                         a[i, j - 1].Appear = a[i, j - 1].Appear + 1;
                         a[i, j].Appear = 0;
                         ScorePlayer2Point = (int)Math.Pow(2, a[i, j - 1].Appear);
                         ScorePlayer2.Instance.ScoreUpdate += ScorePlayer2Point;
                     }
                     if (a[i, j].Appear != a[i, j - 1].Appear && a[i, j].Appear != 0 && a[i, j - 1].Appear != 0)
                     {
                         TurnLimit += 1;
                     }

                 }
             }
             n--;
         }
         ScorePlayer2.Instance.TurnUpdate -= 1;
         if (TurnLimit == 24)//Can/t move there
         {
             ScorePlayer2.Instance.TurnUpdate -= 1;
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



         if (ScorePlayer2.Instance.TurnUpdate == 0)
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
        TilePlayer2[,] temp = new TilePlayer2[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                temp[i, j] = TilesPlayer2[i, j];

            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                TilesPlayer2[i, j] = temp[j, 4 - i - 1];
            }
        }

    }

    void RotateRight()
    {
        TilePlayer2[,] temp = new TilePlayer2[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {

                temp[i, j] = TilesPlayer2[i, j];

            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                TilesPlayer2[i, j] = temp[4 - j - 1, i];

            }
        }
    }

    void MoveLeft()
    {
        MoveToLeft(TilesPlayer2);
    }

    void MoveRight()
    {
        RotateLeft();
        RotateLeft();
        MoveToLeft(TilesPlayer2);
        RotateRight();
        RotateRight();

    }

    void MoveUp()
    {
        RotateLeft();
        MoveToLeft(TilesPlayer2);
        RotateRight();

    }

    void MoveDown()
    {
        RotateRight();
        MoveToLeft(TilesPlayer2);
        RotateLeft();
    }

    public void ReturnToMenuAction()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Move(DirectionPlayer2 d)
    {
        preTilesPlayer2();
        Debug.Log(d.ToString() + " move.");
        switch (d)
        {
            case DirectionPlayer2.Up:
                MoveUp();
                break;
            case DirectionPlayer2.Down:
                MoveDown();
                break;
            case DirectionPlayer2.Right:
                MoveRight();
                break;
            case DirectionPlayer2.Left:
                MoveLeft();
                break;
        }

        Generate();
    }


}
