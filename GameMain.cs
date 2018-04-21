using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Test issue/1

public class GameMain : MonoBehaviour {

    public Text GameOverText;
    public Text YourScore;
    public GameObject GameOverPanel;

    private int ScorePoint;

    private Tile[,] Tiles = new Tile[4, 4];
    List<Tile[]> TableTile = new List<Tile[]>();
    


    void Start()
    {
        //This line returns the numbers of Tiles in the Scene which is on the UI (Total:16)
        GameOverPanel.SetActive(false);
        Tile[] AllTilesAtOnce = GameObject.FindObjectsOfType<Tile>(); //this method is used to get all the tiles in our scene when it starts. But this array will be gone out of this method Start()
        for (int i = 0; i < AllTilesAtOnce.Length; i++)
        {
            AllTilesAtOnce[i].Appear=0;
            Tiles[AllTilesAtOnce[i].Row, AllTilesAtOnce[i].Column] = AllTilesAtOnce[i];
            Debug.Log(AllTilesAtOnce[i].Row+ " " + AllTilesAtOnce[i].Column + " "+i);
        }
        Generate();
    }

    void Update()
    {
       
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
    void Generate()
    {
            int i = UnityEngine.Random.Range(0, 4);
            int j = UnityEngine.Random.Range(0, 4);


            if (Tiles[i, j].Appear == 0)
            {
                Tiles[i, j].Appear = UnityEngine.Random.Range(1, 3);
            }
            else if (Tiles[i, j].Appear != 0)
            {
            Generate();
            }

    }

    private void GameOver()
    {
        YourScore.text = Score.Instance.ScoreUpdate.ToString();
        GameOverPanel.SetActive(true);
        
    }


    void MoveToLeft(Tile[,] a)
    {
        int TurnLimit = 0;
        int n = 2;
        while (n != 0)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j > 0; j--)
                {
                    
                    if (a[i, j].Appear != 0 && a[i, j - 1].Appear == 0)
                    {
                        a[i, j - 1].Appear = a[i, j].Appear;
                        a[i, j].Appear = 0;
                    }
                     if (a[i, j].Appear == a[i, j - 1].Appear && a[i, j-1].Appear != 0 && a[i, j].Appear != 0)
                    {

                        a[i, j -1].Appear = a[i, j - 1].Appear +1;
                        a[i, j].Appear = 0;
                        ScorePoint = (int)Math.Pow(2, a[i, j - 1].Appear);
                        Score.Instance.ScoreUpdate += ScorePoint;
                    }
                    if(a[i, j].Appear != a[i, j-1].Appear && a[i, j - 1].Appear != 0 && a[i, j].Appear != 0)
                    {
                        TurnLimit += 1;
                    }

                }
            }
            n--;
        }
        Score.Instance.TurnUpdate += 1;
        if (TurnLimit == 24)//Can't move there
        {
            Score.Instance.TurnUpdate -= 1;
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
                        if (a[i, j].Appear != a[i + 1, j].Appear && a[i, j].Appear != a[i, j - 1].Appear && a[i, j - 1].Appear != 0 && a[i, j].Appear != 0 && a[i + 1, j].Appear != 0)
                        {
                            GOCount += 1;
                        }
                    }
                    
                }
            }
            n--;
        }
        
        if (GOCount == (18))
        {
            for (int k = 0; k < 6; k++)
            {
                if (a[0, 0].Appear != a[1, 0].Appear && a[1, 0].Appear != a[2, 0].Appear && a[2, 0].Appear != a[3, 0].Appear && 
                    a[3, 3].Appear != a[3, 2].Appear && a[3, 2].Appear != a[3, 1].Appear && a[3, 1].Appear != a[3, 0].Appear
                    )
                    //a[3, 3].Appear != a[2, 3].Appear && a[3, 2].Appear != a[2, 2].Appear && a[3, 1].Appear != a[2, 1].Appear
                {
                    GameOver();
                    break;
                }
            }
            
        }

    }

    void RotateLeft()
    {
        Tile[,] temp = new Tile[4,4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                temp[i, j]= Tiles[i, j];
                
            }
        }
      
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Tiles[i,j] = temp[4 - j - 1, i];
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
                Tiles[i,j] = temp[j, 4 - i - 1];   
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
        RotateRight();
        MoveLeft();
        RotateLeft();
    }

    void MoveDown()
    {
        RotateLeft();
        MoveLeft();
        RotateRight();
    }

    public void Move(Direction d)
    {
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
