using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;

	private Transform cannon;

	private float cannonMovementCount;

	private float startShootingForce;

	[Header("Player Key Input")]
	public string down;
	public string up;
	public string shoot;

	[Header("Rotation")]
	public float maxUpRotation;
	public float maxDownRotation;
	[Space(10)]
	public float moveDown;
	public float moveUp;

	[Header("Ammunation")]
	public GameObject potatoPrefab;
	public Transform potatoSpawn;

	[Header("Shooting Force")]
	public float shootingForce;
	public float maxShootingForce;

	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D>();
		cannon = rb2d.gameObject.transform.GetChild (0).GetComponent<Transform> ();

		cannonMovementCount = 0;

		startShootingForce = shootingForce;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (down)) {
			if (cannonMovementCount != maxDownRotation) {
				cannon.Rotate (0, 0, moveDown);
				cannonMovementCount += Mathf.Abs(moveDown);
			}
		} else if (Input.GetKey (up)) {
			if (cannonMovementCount != maxUpRotation) {
				cannon.Rotate (0, 0, moveUp);
				cannonMovementCount -= Mathf.Abs(moveUp);
			}
		} else if (Input.GetKey (shoot)) {
			IncreaseForce ();
		} else if (Input.GetKeyUp (shoot)) {
			Fire ();
			shootingForce = startShootingForce;
		}
	}

	void IncreaseForce () {
		// This increases the force, needs animation to show force being increased!!
		if (shootingForce <= maxShootingForce) {
			shootingForce += 0.1f;
		}
	}

	void Fire() {
		// Create the Potato from the Potato Prefab
		var potato = (GameObject)Instantiate (
			potatoPrefab,
			potatoSpawn.position,
			cannon.rotation);

		// Add velocity to the potato
		potato.GetComponent<Rigidbody2D>().velocity = potato.transform.up * shootingForce;

		// Destroy the potato after 5 seconds
		Destroy(potato, 5.0f);
	}
}
