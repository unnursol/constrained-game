using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

	private float[] spawnPlacement =  {400f, -400f};
	private float placement;

	void Awake () {
		placement = spawnPlacement [Random.Range (0, spawnPlacement.Length)];
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

	void Fall() {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -100f, 0), 1f);
		if (transform.position.y == 100f) {
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
