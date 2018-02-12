﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FlyingObject : MonoBehaviour {

	private float[] spawnPlacement =  {40f, -40f};
	private float placement;
    public Text P1ScoreText;
    public Text P2ScoreText;

    void Awake ()
    {
		placement = spawnPlacement [Random.Range (0, spawnPlacement.Length)];
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		if (placement < 0)
			sr.flipX = true;
		transform.Translate (placement, 0, 0);
		Fly ();
	}

	void Fly() {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(-placement, transform.position.y, 0), 0.2f);
		if (transform.position.x == -placement) {
			Destroy (gameObject);
			return;
		}
		Invoke ("Fly", 0.02f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
	    int score = 0;
		if (other.tag == "ammunation1") {
			FlyUp ();
		    score = Int32.Parse(P1ScoreText.text);
            P1ScoreText.text = (score++).ToString();

            Destroy (other.gameObject);	    
		}
        else if(other.tag == "ammunation2")
	    {
	        FlyUp();
	        score = Int32.Parse(P2ScoreText.text);
	        P2ScoreText.text = (score++).ToString();
            Destroy(other.gameObject);	        
	    }
	}

	void FlyUp() {
		if (transform.childCount > 0) {
			Destroy (transform.GetChild (0).gameObject);
		}

		transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 30f, 0), 0.2f);

		if (transform.position.y == 30f) {
			Destroy (gameObject);
			return;
		}
		Invoke ("FlyUp", 0.02f);
	}
}
