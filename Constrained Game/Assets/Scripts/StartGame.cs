using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	private bool startGame;
	private bool endGame;
    private bool winGame;
    
    //private Time time;
    public Text timerText;
    public float startTime = 0;


    public GameObject startScreen;
	public GameObject endScreen;
    public GameObject winScreen;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		startGame = false;
		endGame = false;
        winGame = false;
        startScreen.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (!startGame && Input.anyKey) {
			startScreen.SetActive (false);
            startTime = Time.time;
            startGame = true;
			Time.timeScale = 1;
		}
		// This is to test end menu
		if (!endGame && Input.GetKeyDown ("escape")) {
			endScreen.SetActive (true);
			endGame = true;
			Time.timeScale = 0;
		}

        UpdateTimer();
        
	}

    private void UpdateTimer()
    {
        float t = Time.time - startTime;
        if (t >= 2)
        {
            Time.timeScale = 0;
            winGame = true;
            winScreen.SetActive(true);
        }
        else
        { 
            string minutes, seconds;
            if (startScreen.active)
            {
                minutes = (2).ToString();
                seconds = (0).ToString();
            }
            else
            {            
                minutes = (1 - ((int)t / 60)).ToString();
                seconds = (60 - (t % 60)).ToString("f0");
            }

            timerText.text = minutes + ":" + seconds;
        }
    }
}
