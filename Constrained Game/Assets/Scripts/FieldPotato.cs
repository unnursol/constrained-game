using System.Collections;
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
    private GameController gameController;
    private Collider2D collider;
    private int player;

    void Start(){

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        collider = GetComponent<Collider2D>();

        if(transform.parent.tag == "potatoFarmingRight")
        {
            player = 1;
        }

        else if(transform.parent.tag == "potatoFarmingLeft")
        {
            player = 2;
        }

        maxLevel = potatoes.Length;
		lastUpdate = Time.time;
	}

	void Update(){
		if (lastUpdate <= Time.time - growUpdateTime) {
			levelUp ();
		}
        if(level == 0 && collider.enabled)
        {
            collider.enabled = false;
        }
        else if(level > 0 && !collider.enabled)
        {
            collider.enabled = true;
        }

        if(level == maxLevel)
        {
            Harvest();
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

    public void levelZero()
    {
        level = 0;
        spriteRenderer.sprite = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ammunation1" && player == 2)
        {
            levelZero();
            Destroy(other.gameObject);
        }
        else if (other.tag == "ammunation2" && player == 1)
        {
            levelZero();
            Destroy(other.gameObject);
        }
    }

    void Harvest()
    {
        gameController.HarvestPotato(player);
        levelZero();
    }
}
