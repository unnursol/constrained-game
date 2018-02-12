﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldPotato : MonoBehaviour {

	public Sprite[] potatoes;
	public SpriteRenderer spriteRenderer;
	public int level;
	private int maxLevel;
	public float growUpdateTime;
	private float lastUpdate;

	void Start(){
		maxLevel = potatoes.Length;
		lastUpdate = Time.time;
	}

	void Update(){
		Debug.Log (lastUpdate);
		Debug.Log (Time.time);
		if (lastUpdate <= Time.time - growUpdateTime) {
			levelUp ();
		}
	}

	public void levelUp(){
		lastUpdate = Time.time;
		level = level < maxLevel ? level + 1 : level;
		if (level != 0) {
			spriteRenderer.sprite = potatoes [level - 1];
		}
	}

	public void levelDown(){
		lastUpdate = Time.time;
		level = level == 0 ? 0 : level - 1;
		if (level == 0) {
			spriteRenderer.sprite = null;
		} else {
			spriteRenderer.sprite = potatoes [level - 1];
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "ammunation") {
			Destroy (other.gameObject);
			levelDown ();
		}
	}
}
