using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private bool startGame;
    private bool endGame;
    private bool winGame;

    [Header("Timer")]
    public Text timerText;
    public float startTime;


    [Header("Score")]
    public int p1score;
    public int p2score;
    public Text p1ScoreText;
    public Text p2ScoreText;

    [Header("Farming")]
    public Transform p1Bag;
    public Transform p2Bag;
    public GameObject smallPotato;

    public GameObject p1crops;
    public GameObject p2crops;

    public GameObject rainObj;
    private Rain rain;



    [Header("Win Screen Variables")]
    public Text winner;
    public Text message;


    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject winScreen;

    // Use this for initialization
    void Start()
    {
        if (rainObj != null)
        {
            rain = rainObj.GetComponent<Rain>();
        }

        Time.timeScale = 0;
        startTime = Time.time;
        startGame = false;
        endGame = false;
        winGame = false;
        p1score = 0;
        p2score = 0;
        startScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startGame && Input.anyKey)
        {
            startScreen.SetActive(false);
            startTime = Time.time;
            startGame = true;
            Time.timeScale = 1;
        }
        // This is to test end menu
        if (!endGame && Input.GetKeyDown("escape"))
        {
            endScreen.SetActive(true);
            endGame = true;
            Time.timeScale = 0;
        }
        UpdateTimer();

        p1ScoreText.text = p1score.ToString();
        p2ScoreText.text = p2score.ToString();

    }

    public void HarvestPotato(int player)
    {
        if(player == 1)
        {
            Instantiate(smallPotato, p1Bag);
            p1score++;
        }
        else if(player == 2)
        {
            Instantiate(smallPotato, p2Bag);
            p2score++;
        }
    }

    public void HarvestPotatoes(int player)
    {
        if (player == 1)
        {
            foreach (Transform crop in p1crops.transform)
                crop.gameObject.GetComponent<FieldPotato>().Harvest();
        }
        else if (player == 2)
        {
            foreach (Transform crop in p2crops.transform)
                crop.gameObject.GetComponent<FieldPotato>().Harvest();
        }
    }

    public void WaterPotatoes(int player)
    {
        if(player == 1)
        {
            rain.RainRightOn();
            foreach (Transform crop in p1crops.transform)
                crop.gameObject.GetComponent<FieldPotato>().levelUp();
        }
        else if (player == 2)
        {
            rain.RainLeftOn();
            foreach (Transform crop in p2crops.transform)
                crop.gameObject.GetComponent<FieldPotato>().levelUp();
        }
    }

    private void UpdateTimer()
    {
        float t = Time.time - startTime;
        if (t >= 120 || (p1score >= 50 || p2score >= 50))
        {
            UnityEngine.Debug.Log(t);
            Time.timeScale = 0;
            UpdateWinner();

            winGame = true;
            winScreen.SetActive(true);
        }
        else
        {
            string minutes, seconds;
            if (startScreen.active)
            {
                minutes = "02";
                seconds = "00";
            }
            else
            {
                minutes = "0" + (1 - ((int)t / 60)).ToString();
                seconds = (60 - (t % 60)).ToString("f0");
                if(Int32.Parse(seconds) < 10)
                {
                    seconds = "0" + seconds;
                }
            }

            timerText.text = minutes + ":" + seconds;
        }
    }

    private void UpdateWinner()
    {
        if(p2score == p1score)
        {
            winner.text = "IT'S A DRAW!";
            message.text = "Neither of you managed to gain the upper hand and are therefore forced to compete in price, " +
                "constantly lowering to gain the upper hand. Because of this you couldn't afford seeds " +
                "for next years crops and both went out of business. Try again?";
        }
        else
        {
            string looser = "";
            if (p2score > p1score)
            {
                winner.text = "WINNER IS: PLAYER 1!";
                looser = "player 2";
            }
            else if (p2score < p1score)
            {
                winner.text = "WINNER IS: PLAYER 2!";
                looser = "Player 1";
            }
            message.text = "You have the upper hand and control the market price of potatoes, lowering it until " + looser + " goes out of business. " +
                           "You now have potato monopoly and make millions! " +
                           "" + looser + " asks you for a job which you happily give him.... as a butler.";
        }
    }
}
