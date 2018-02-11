using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawn : MonoBehaviour {

	[Header("Drop Prefabs")]
	public GameObject[] drops;

	[Header("Bird bags")]
	public GameObject[] bags;

	[Header("Balloon crates")]
	public GameObject[] balloonCrates;

	[Header("Drone crates")]
	public GameObject[] DroneCrates;

	public float spawnMin;
	public float spawnMax;
	public float secondsBetweenSpawn;

	// Use this for initialization
	void Start () {
		StartCoroutine (spawn ());
	}

	// Instantiates a given spawn object every 2-4 seconds.
	void Spawn() {

		GameObject spawnObj = drops [Random.Range (0, drops.Length)];

		Instantiate (spawnObj, spawnObj.transform.position, Quaternion.identity);

		Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}

	private IEnumerator spawn(){
		while (true) {
			yield return new WaitForSeconds (secondsBetweenSpawn);
			GameObject spawnObj = drops [Random.Range (0, drops.Length)];
			Instantiate (spawnObj, spawnObj.transform.position, Quaternion.identity);
		}
	}
}
