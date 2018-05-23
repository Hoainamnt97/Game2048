using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DirectionExpand
{
    Up, Down, Right, Left
}

public class KeyBoardForLevelUp : MonoBehaviour
{


    private LevelUp lm;

    void Awake()
    {
        lm = GameObject.FindObjectOfType<LevelUp>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            lm.Move(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            lm.Move(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            lm.Move(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            lm.Move(Direction.Left);
        }
    }


}
