using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;

	private Transform cannon;

	private float downCount;
	private float upCount;

	private float startShootingForce;

	[Header("Player Key Input")]
	public string cannonDown;
	public string cannonUp;
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

		downCount = 0;
		upCount = 0;

		startShootingForce = shootingForce;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (cannonDown)) {
			if (downCount != maxDownRotation) {
				cannon.Rotate (0, 0, moveDown);
				downCount += moveDown;
				upCount -= moveUp;
			}
		} else if (Input.GetKey (cannonUp)) {
			if (upCount != maxUpRotation) {
				cannon.Rotate (0, 0, moveUp);
				upCount += moveUp;
				downCount -= moveDown;
			}
		} else if (Input.GetKey (shoot)) {
			IncreaseForce ();
		} else if (Input.GetKeyUp (shoot)) {
			Fire ();
			shootingForce = startShootingForce;
		}
	}

	void IncreaseForce () {
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
