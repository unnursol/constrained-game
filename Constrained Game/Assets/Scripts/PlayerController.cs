using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	protected Rigidbody2D rb2d;

    //protected Transform cannon;

    protected float cannonMovementCount;

	protected float shootingForce;

	protected bool increaseForce;

    protected List<Renderer> forceCircles;

    protected int forceCirclesCount = 11;

    protected Animator anim;

	[Header("Player Key Input")]
	public string down;					// Key Input for cannon down
	public string up;					// Key Input for cannon up
	public string shoot;				// Key Input for cannon shoot

	[Header("Rotation")]
	public float maxUpRotation;			// Maximum up rotation for cannon
	public float maxDownRotation;		// Maximum down rotation for cannon
	[Space(10)]
	public float move;					// How fast should the cannon move, negative for player on left side of
	// screen and positive for player on right side of screen

	[Header("Ammunation")]
	public GameObject potatoPrefab;
	public Transform potatoSpawn;		// Potato prefab should be attached to the end of the gun


	[Header("Shooting Force")]
	public float forceIncrementPerSecond;
	public float minShootingForce;
	public float maxShootingForce;
	public float secondsAtMaxForce;
	private float currentSecondsAtMaxForce;
	public Transform forceIndicator;

    [Header("Weapons")]
    public Transform cannon;

    [Header("Force Text UI")]
	public Text forceText;				// This is temporary to see the force of the shooting, change with animation later

	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D>();
		//cannon = rb2d.gameObject.transform.GetChild (0).GetComponent<Transform> ();

		cannonMovementCount = 0;

		shootingForce = minShootingForce;

		increaseForce = true;

		forceText.text = "Force: ";

		currentSecondsAtMaxForce = secondsAtMaxForce;

	    ForceIndicatorInit();

        anim = cannon.GetComponent<Animator>();
	}


    // Update is called once per frame
	void Update ()
	{

	    ForceIndicatorHandler();

		if (Input.GetKey (down)) {
			if (cannonMovementCount != maxDownRotation) {
				cannon.Rotate (0, 0, move);
				cannonMovementCount += Mathf.Abs(move);
			}
			if(Input.GetKey (shoot)) {
				if (increaseForce)
					IncreaseForce ();
				else
					DecreaseForce ();
			}
		} else if (Input.GetKey (up)) {

			if (cannonMovementCount != maxUpRotation) {
				cannon.Rotate (0, 0, -move);
				cannonMovementCount -= Mathf.Abs(move);
			}
			if(Input.GetKey (shoot)) {
				if (increaseForce)
					IncreaseForce ();
				else
					DecreaseForce ();
			}
		} else if (Input.GetKey (shoot)) {
			if (increaseForce)
				IncreaseForce ();
			else
				DecreaseForce ();

			forceText.text = "Force: " + shootingForce;
		} else if (Input.GetKeyUp (shoot)) {
			Fire ();
			shootingForce = minShootingForce;
		}
	}

    void IncreaseForce () {
		// This increases the force, needs animation to show force being increased!!
		if (shootingForce <= maxShootingForce) {
			currentSecondsAtMaxForce = secondsAtMaxForce;
			shootingForce += forceIncrementPerSecond * Time.deltaTime;
		}
		else if (currentSecondsAtMaxForce > 0f) {
			currentSecondsAtMaxForce -= Time.deltaTime;
			if (currentSecondsAtMaxForce < 0f) {
				increaseForce = false;
			}
		}
	}

	void DecreaseForce () {
		// This decreses the force, needs animation to show force being decreased!!
		if (shootingForce >= minShootingForce)
			shootingForce -= forceIncrementPerSecond * Time.deltaTime;
		else
			increaseForce = true;
	}

	void Fire() {

        anim.SetTrigger("Shoot");

		// Create the Potato from the Potato Prefab
		var potato = (GameObject)Instantiate (
			potatoPrefab,
			potatoSpawn.position,
			cannon.rotation);

		GetComponent<AudioSource> ().Play ();

		// Add velocity to the potato
		potato.GetComponent<Rigidbody2D>().velocity = potato.transform.up * shootingForce;

		// Destroy the potato after 5 seconds
		Destroy(potato, 5.0f);
	}

    private void ForceIndicatorHandler()
    {
        float gap = maxShootingForce / forceCirclesCount;

        for (int i = 0; i < forceCirclesCount; i++)
        {
            if (shootingForce >= i * gap)
            {
                forceCircles[i].enabled = true;
            }
            else
            {
                forceCircles[i].enabled = false;
            }
        }
    }


    private void ForceIndicatorInit()
    {
        forceCircles = new List<Renderer>();
        for (int i = 0; i < forceCirclesCount; i++)
        {
            forceCircles.Add(forceIndicator.GetChild(i).GetComponent<Renderer>());
        }
    }
}
