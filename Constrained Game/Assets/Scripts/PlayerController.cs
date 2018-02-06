using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		/* if (Input.GetKeyDown ("a")) {
			float x = rb2d.transform.position.x;
			float y = rb2d.transform.position.y;

			rb2d.MovePosition (new Vector2 (x + 0.2, y));
		}*/
	}
}
