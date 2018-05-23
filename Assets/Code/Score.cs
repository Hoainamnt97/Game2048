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
    public Text Top1;
    public Text Top2;
    public Text Top3;
    public Text Top1Score;
    public Text Top2Score;
    public Text Top3Score;


    void Awake()
    {
        Instance = this;
        ScorePoint.text = "0";
        TurnPoint.text = "0";



        if (!PlayerPrefs.HasKey("Top1Score"))
            PlayerPrefs.SetInt("Top1Score", 3000);
        if (!PlayerPrefs.HasKey("Top2Score"))
            PlayerPrefs.SetInt("Top2Score", 2000);
        if (!PlayerPrefs.HasKey("Top3Score"))
            PlayerPrefs.SetInt("Top3Score", 1000);
        if (!PlayerPrefs.HasKey("Top1"))
            PlayerPrefs.SetString("Top1", "Nam");
        if (!PlayerPrefs.HasKey("Top2"))
            PlayerPrefs.SetString("Top2", "Thang");
        if (!PlayerPrefs.HasKey("Top3"))
            PlayerPrefs.SetString("Top3", "Phu");

        Top1Score.text = PlayerPrefs.GetInt("Top1Score").ToString();
        Top2Score.text = PlayerPrefs.GetInt("Top2Score").ToString();
        Top3Score.text = PlayerPrefs.GetInt("Top3Score").ToString();
        Top1.text = PlayerPrefs.GetString("Top1").ToString();
        Top2.text = PlayerPrefs.GetString("Top2").ToString();
        Top3.text = PlayerPrefs.GetString("Top3").ToString();
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

            if (PlayerPrefs.GetInt("Top1Score") < score)
            {
                PlayerPrefs.SetInt("Top1Score", score);
                PlayerPrefs.SetString("Top1", PlayerPrefs.GetString("PlayerName").ToString());
                Top1Score.text = score.ToString();
                Top1.text = PlayerPrefs.GetString("PlayerName").ToString();
            }
            else if (PlayerPrefs.GetInt("Top2Score") < score)
            {
                PlayerPrefs.SetInt("Top2Score", score);
                PlayerPrefs.SetString("Top2", PlayerPrefs.GetString("PlayerName").ToString());
                Top2Score.text = score.ToString();
                Top2.text = PlayerPrefs.GetString("PlayerName").ToString();
            }
            else if (PlayerPrefs.GetInt("Top3Score") < score)
            {
                PlayerPrefs.SetInt("Top3Score", score);
                PlayerPrefs.SetString("Top3", PlayerPrefs.GetString("PlayerName").ToString());
                Top3Score.text = score.ToString();
                Top3.text = PlayerPrefs.GetString("PlayerName").ToString();
            }
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
