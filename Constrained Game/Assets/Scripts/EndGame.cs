﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown ("space") || Input.GetKeyDown("r")) {
			SceneManager.LoadScene (0);
		} else if (Input.GetKeyDown ("escape")) {
			Application.Quit ();
		}
	}
}