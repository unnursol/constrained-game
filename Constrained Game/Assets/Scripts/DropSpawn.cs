﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawn : MonoBehaviour {

	[Header("Drop Prefabs")]
	public GameObject[] drops;

	public float spawnMin = 2f;
	public float spawnMax = 4f;

	// Use this for initialization
	void Start () {
		Spawn ();
	}

	void Spawn() {

		GameObject spawnObj = drops [Random.Range (0, drops.Length)];

		spawnObj = Instantiate (spawnObj, spawnObj.transform.position, Quaternion.identity);

		Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}
}
