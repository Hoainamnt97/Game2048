using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePlayer1 : MonoBehaviour
{


    private int score;
    private int turn;
    public static ScorePlayer1 Instance;
    public Text ScorePoint;
    public Text TurnPoint;


    void Awake()
    {
        Instance = this;
        ScorePoint.text = "0";
        TurnPoint.text = "0";

    }
    public int ScoreUpdate
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            ScorePoint.text = score.ToString();
        }
    }

    public int TurnUpdate
    {
        get
        {
            return turn;
        }

        set
        {
            turn = value;
            TurnPoint.text = turn.ToString();
        }
    }

}
