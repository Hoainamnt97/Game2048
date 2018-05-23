using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class ScoreForDemo : MonoBehaviour {


    private int score;
    private int turn;
    public static ScoreForDemo Instance;
    public Text ScorePoint;
    public Text TurnPointDemo;

    void Awake()
    {
        Instance = this;
        ScorePoint.text = "0";
        TurnPointDemo.text = "0";

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
            TurnPointDemo.text = turn.ToString();
        }
    }

}
