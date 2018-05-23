using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DirectionPlayer1
{
    Up, Down, Right, Left
}

public class KeyBoardPlayer1 : MonoBehaviour
{

    private GameMainPlayer1 gm1;

    void Awake()
    {
        gm1 = GameObject.FindObjectOfType<GameMainPlayer1>();
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gm1.Move(DirectionPlayer1.Up);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gm1.Move(DirectionPlayer1.Down);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gm1.Move(DirectionPlayer1.Right);

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gm1.Move(DirectionPlayer1.Left);

        }
    }


}
