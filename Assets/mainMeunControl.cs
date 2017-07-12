using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMeunControl : MonoBehaviour {

    public Text high;//hold the UI text for score
	void Start () {
        highScore();
	}
    //load the game scence 
    public void play()
    {
        SceneManager.LoadScene(1);
    }
    //get high score and display it
    public void highScore()
    {
        high.text = PlayerPrefs.GetInt("HighScore").ToString() ;
    }
}
