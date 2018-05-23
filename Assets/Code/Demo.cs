using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    float timeToGo;
   // public AudioClip AudioClip;
    //public AudioSource AudioSource;
    public Text GameOverText;
    public Text YourScore;
    public GameObject GameOverPanel;
    public GameObject GamePanel;
    private int ScorePoint;

    private Tile[,] Tiles = new Tile[4, 4];
    Stack<int> previousTiles = new Stack<int>();
    void Start()
    {
        timeToGo = Time.fixedTime + 0.5f;
        //This line returns the numbers of Tiles in the Scene which is on the UI (Total:16)
        ScoreForDemo.Instance.ScoreUpdate = 0;
        ScoreForDemo.Instance.TurnUpdate = 0;
        GameOverPanel.SetActive(false);
        //  GamePanel.SetActive(true);
        Tile[] AllTilesAtOnce = GameObject.FindObjectsOfType<Tile>(); //this method is used to get all the Tiles in our scene when it starts. But this array will be gone out of this method Start()
        for (int i = 0; i < AllTilesAtOnce.Length; i++)
        {
            AllTilesAtOnce[i].Appear = 0;
            Tiles[AllTilesAtOnce[i].Row, AllTilesAtOnce[i].Column] = AllTilesAtOnce[i];

        }
        Generate();
        Generate();
    }

    void Update()
    {

        if (Time.fixedTime >= timeToGo)
        {
            var enumValues = Enum.GetValues(typeof(Direction));
            var random = new System.Random();
            var randomValue = (Direction)enumValues.GetValue(random.Next(enumValues.Length));
            timeToGo = Time.fixedTime + 0.5f;
            Move(randomValue);
        }
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        Start();
    }


    public void preTiles()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                previousTiles.Push(Tiles[i, j].Appear);

            }
        }

    }

    public void Undo()
    {
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                Tiles[i, j].Appear = previousTiles.Pop();
            }
        }

    }
    void Generate()
    {
        int i = UnityEngine.Random.Range(0, 4);
        int j = UnityEngine.Random.Range(0, 4);


        if (Tiles[i, j].Appear == 0)
        {
            Tiles[i, j].Appear = UnityEngine.Random.Range(1, 2);
        }
        else if (Tiles[i, j].Appear != 0)
        {
            Generate();
        }

    }
    private void GameOver()
    {

        // GamePanel.SetActive(false);
        YourScore.text = ScoreForDemo.Instance.ScoreUpdate.ToString();
        GameOverPanel.SetActive(true);
        
    }



    void MoveToLeft(Tile[,] a)
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

                    //merge and score
                    if (a[i, j].Appear == a[i, j - 1].Appear && a[i, j - 1].Appear != 0 && a[i, j].Appear != 0)
                    {
                        a[i, j - 1].Appear = a[i, j - 1].Appear + 1;
                        a[i, j].Appear = 0;
                        ScorePoint = (int)Math.Pow(2, a[i, j - 1].Appear);
                        ScoreForDemo.Instance.ScoreUpdate += ScorePoint;
                        //AudioSource.Play();
                    }
                    if (a[i, j].Appear != a[i, j - 1].Appear && a[i, j].Appear != 0 && a[i, j - 1].Appear != 0)
                    {
                        TurnLimit += 1;
                    }

                }
            }
            n--;
        }
        ScoreForDemo.Instance.TurnUpdate += 1;
        if (TurnLimit == 24)//Can/t move there
        {
            ScoreForDemo.Instance.TurnUpdate -= 1;
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




        /* if (Score.Instance.TurnUpdate == 0)
         {
             GameOver();

         }
         else*/
        if (GOCount == (18))
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

    }



    void RotateLeft()
    {
        Tile[,] temp = new Tile[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                temp[i, j] = Tiles[i, j];

            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Tiles[i, j] = temp[j, 4 - i - 1];
            }
        }

    }

    void RotateRight()
    {
        Tile[,] temp = new Tile[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {

                temp[i, j] = Tiles[i, j];

            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Tiles[i, j] = temp[4 - j - 1, i];

            }
        }
    }

    void MoveLeft()
    {
        MoveToLeft(Tiles);
    }

    void MoveRight()
    {
        RotateLeft();
        RotateLeft();
        MoveToLeft(Tiles);
        RotateRight();
        RotateRight();

    }

    void MoveUp()
    {
        RotateLeft();
        MoveToLeft(Tiles);
        RotateRight();

    }

    void MoveDown()
    {
        RotateRight();
        MoveToLeft(Tiles);
        RotateLeft();
    }

    public void ReturnToMenuAction()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Move(Direction d)
    {

        preTiles();
        Debug.Log(d.ToString() + " move.");
        switch (d)
        {
            case Direction.Up:
                MoveUp();
                break;
            case Direction.Down:
                MoveDown();
                break;
            case Direction.Right:
                MoveRight();
                break;
            case Direction.Left:
                MoveLeft();
                break;
         
        }

        Generate();
    }


}
