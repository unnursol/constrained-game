using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldPotato : MonoBehaviour {

	public Sprite[] potatoes;
	public SpriteRenderer spriteRenderer;
	public int level;
	private int maxLevel;

	void Start(){
		maxLevel = potatoes.Length;
	}

	public void levelUp(){
		level = level < maxLevel ? level + 1 : level;
		if (level != 0) {
			spriteRenderer.sprite = potatoes [level - 1];
		}
	}

	public void levelDown(){
		level = level == 0 ? 0 : level - 1;
		if (level == 0) {
			spriteRenderer.sprite = null;
		} else {
			spriteRenderer.sprite = potatoes [level - 1];
		}
	}
}
