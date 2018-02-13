using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {

    public float rainTime;
    private float lastUpdateRight;
    private float lastUpdateLeft;

    private bool rainRightOn;
    private bool rainLeftOn;

    public GameObject rainRight;
    public GameObject rainLeft;

    // Use this for initialization
    void Start () {
        lastUpdateLeft = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if(rainLeftOn && lastUpdateLeft <= Time.time - rainTime)
        {
            rainLeftOn = false;
            rainLeft.SetActive(false);
        }

        if (rainRightOn && lastUpdateRight <= Time.time - rainTime)
        {
            rainRightOn = false;
            rainRight.SetActive(false);
        }
    }

    public void RainRightOn()
    {
		GetComponent<AudioSource> ().Play ();
        rainRightOn = true;
        lastUpdateRight = Time.time;
        rainRight.SetActive(true);
    }

    public void RainLeftOn()
    {
		GetComponent<AudioSource> ().Play ();
        rainLeftOn = true;
        lastUpdateLeft = Time.time;
        rainLeft.SetActive(true);
    }
}
