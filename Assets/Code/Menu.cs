using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    
    public Button Play;
    public Button LevelUp;
    public Button Exit;
    public Button PlayerVersus;
    public Button Demo;
    public InputField PlayerName;
    public Text Welcome;


    private void Start()
    {
        Play.onClick.AddListener(() => { SceneManager.LoadScene("Main"); });
        LevelUp.onClick.AddListener(() => { SceneManager.LoadScene("LevelUp"); });
        PlayerVersus.onClick.AddListener(() => { SceneManager.LoadScene("PlayerVersus"); });
        Demo.onClick.AddListener(() => { SceneManager.LoadScene("Demo"); });
        Exit.onClick.AddListener(() => { Application.Quit(); });

        
        PlayerName.onEndEdit.AddListener(setPlayerName);
    }

    void Awake()
    {

        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerPrefs.SetString("PlayerName", "Anonymous");
        }

        Welcome.text = "Welcome:  <b>" + PlayerPrefs.GetString("PlayerName").ToString() + "</b>";
    }
    
    public void setPlayerName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        Welcome.text = "Welcome:  <b>" + PlayerPrefs.GetString("PlayerName").ToString() + "</b>";
    }

}
