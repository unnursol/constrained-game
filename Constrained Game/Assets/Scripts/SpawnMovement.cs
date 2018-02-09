using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovement : MonoBehaviour {

	private float[] spawnPlacement =  {5.5f, -5.5f};
	private float placement;

	void Awake () {
		placement = spawnPlacement [Random.Range (0, spawnPlacement.Length)];
		transform.Translate (placement, 0, 0);
		Fly ();
	}

	void Fly() {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(-placement, transform.position.y, 0), 0.01f);
		if (transform.position.x == -placement) {
			Destroy (gameObject);
			return;
		}
		Invoke ("Fly", 0.02f);
	}

	void Fall() {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -7.2f, 0), 0.01f);
		if (transform.position.y == 7.2f) {
			Destroy (gameObject);
			return;
		}
		Invoke ("Fall", 0.02f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "ammunation") {
			Fall ();
		}
	}

}
