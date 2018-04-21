using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class Score : MonoBehaviour {


    private int score;
    private int turn;
    public static Score Instance;
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
