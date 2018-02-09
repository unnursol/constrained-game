using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	private bool startGame;

	public GameObject startScreen;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		startGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!startGame && Input.anyKey) {
			startScreen.SetActive (false);
			Time.timeScale = 1;
		}
	}
}
