using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	private bool startGame;
	private bool endGame;

	public GameObject startScreen;
	public GameObject endScreen;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		startGame = false;
		endGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!startGame && Input.anyKey) {
			startScreen.SetActive (false);
			startGame = true;
			Time.timeScale = 1;
		}
		// This is to test end menu
		if (!endGame && Input.GetKeyDown ("escape")) {
			endScreen.SetActive (true);
			endGame = true;
			Time.timeScale = 0;
		}
	}
}
