using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyBoardForDemo : MonoBehaviour
{


    private Demo dm;

    void Awake()
    {
        dm = GameObject.FindObjectOfType<Demo>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            dm.Move(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            dm.Move(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            dm.Move(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            dm.Move(Direction.Left);
        }
    }


}
