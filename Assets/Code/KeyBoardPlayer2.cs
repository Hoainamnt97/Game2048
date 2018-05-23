using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DirectionPlayer2
{
    Up, Down, Right, Left
}

public class KeyBoardPlayer2 : MonoBehaviour
{

    private GameMainPlayer2 gm2;

    void Awake()
    {
        gm2 = GameObject.FindObjectOfType<GameMainPlayer2>();
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            gm2.Move(DirectionPlayer2.Up);

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            gm2.Move(DirectionPlayer2.Down);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            gm2.Move(DirectionPlayer2.Right);

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            gm2.Move(DirectionPlayer2.Left);

        }
    }


}
