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


    [Header("Player Variables")]
    public int player1score;
    public int player2score;
    public Text p1ScoreText;
    public Text p2ScoreText;
    public Transform p1Bag;
    public Transform p2Bag;
    public GameObject smallPotato;


    [Header("Win Screen Variables")]
    public Text winner;
    public Text message;


    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject winScreen;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0;
        startTime = Time.time;
        startGame = false;
        endGame = false;
        winGame = false;
        player1score = 0;
        player2score = 0;
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

        p1ScoreText.text = player1score.ToString();
        p2ScoreText.text = player2score.ToString();

    }

    public void HarvestPotato(int player)
    {
        if(player == 1)
        {
            Instantiate(smallPotato, p1Bag);
            player1score++;
        }
        else if(player == 2)
        {
            Instantiate(smallPotato, p2Bag);
            player2score++;
        }

    }

    private void UpdateTimer()
    {
        float t = Time.time - startTime;
        if (t >= 120)
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
        if(player2score == player1score)
        {
            winner.text = "IT'S A DRAW!";
            message.text = "Neither of you managed to gain the upper hand and are therefore forced to compete in price, " +
                "constantly lowering to gain the upper hand. Because of this you couldn't afford seeds " +
                "for next years crops and both went out of business. Try again?";
        }
        else
        {
            string looser = "";
            if (player2score > player1score)
            {
                winner.text = "WINNER IS: PLAYER 1!";
                looser = "player 2";
            }
            else if (player2score < player1score)
            {
                winner.text = "WINNER IS: PLAYER 2!";
                looser = "Player 1";
            }
            message.text = "You have the upper hand and control the market price of potatoes, lowering it until " + looser + " goes out of business. " +
                           "You now have potato monopoly and make millions! " +
                           "" + looser + "'s asks you for a job which you happily give him....as a butler.";
        }     
    }
}