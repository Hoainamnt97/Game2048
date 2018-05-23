using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStatic : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("music");
        if (go.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

