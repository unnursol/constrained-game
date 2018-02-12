﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space") || Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("Main Scene");
        }
        else if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

    }
}