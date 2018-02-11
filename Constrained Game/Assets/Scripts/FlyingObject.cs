using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour {

	private float[] spawnPlacement =  {400f, -400f};
	private float placement;

	void Awake () {
		placement = spawnPlacement [Random.Range (0, spawnPlacement.Length)];
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		if (placement < 0)
			sr.flipX = true;
		transform.Translate (placement, 0, 0);
		Fly ();
	}

	void Fly() {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(-placement, transform.position.y, 0), 1f);
		if (transform.position.x == -placement) {
			Destroy (gameObject);
			return;
		}
		Invoke ("Fly", 0.02f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "ammunation") {
			FlyUp ();
			Destroy (other.gameObject);
		}
	}

	void FlyUp() {
		if (transform.childCount > 0) {
			Destroy (transform.GetChild (0).gameObject);
		}

		transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 400f, 0), 1.5f);

		if (transform.position.y == 400f) {
			Destroy (gameObject);
			return;
		}
		Invoke ("FlyUp", 0.02f);
	}
}
