using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    Up,Down,Right,Left
}

public class KeyBoard : MonoBehaviour {

    private GameMain gm;

    void Awake(){
        gm = GameObject.FindObjectOfType<GameMain>();
        }

  

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gm.Move(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gm.Move(Direction.Down);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            gm.Move(Direction.Right);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gm.Move(Direction.Left);
        }
    }


}
