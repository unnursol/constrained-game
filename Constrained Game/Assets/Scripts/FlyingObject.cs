using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FlyingObject : MonoBehaviour {

	private float[] spawnPlacement =  {40f, -40f};
	private float placement;
    private GameController gameController;

    void Awake ()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

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
		if (other.tag == "ammunation1") {
			FlyUp ();
            gameController.player1score++;
            Destroy (other.gameObject);	    
		}
        else if(other.tag == "ammunation2")
	    {
	        FlyUp();
            gameController.player2score++;
            Destroy(other.gameObject);	        
	    }
	}

	void FlyUp() {
		// If child count is larger than 0 then you find all children and 
		// destroy them leaving only the flying object
		if (transform.childCount > 0) {
			Transform[] children = GetComponentsInChildren<Transform> ();
			for (int i = 1; i < children.Length; i++) {
				Destroy (children[i].gameObject);
			}
		}

		transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 30f, 0), 0.2f);

		if (transform.position.y == 30f) {
			Destroy (gameObject);
			return;
		}
		Invoke ("FlyUp", 0.02f);
	}
}
