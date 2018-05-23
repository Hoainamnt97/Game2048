using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class ScoreForLevelUp : MonoBehaviour {


    private int score;
    private int turn;
    public static ScoreForLevelUp Instance;
    public Text ScorePoint;
    public Text TurnPointExpand;

    void Awake()
    {
        Instance = this;
        ScorePoint.text = "0";
        TurnPointExpand.text = "100";

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
            TurnPointExpand.text = turn.ToString();
        }
    }

}
