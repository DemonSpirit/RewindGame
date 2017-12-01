﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIGetCharacterText : MonoBehaviour
{
    public Text healthTxt,nameTxt;
    public PlayerController activeInst;
    public GameControl gameCtrl;

    // Use this for initialization
    void Start()
    {
        gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();

    }

    // Update is called once per frame
    void Update()
	{   if (gameCtrl.gameState == "pre-live" || gameCtrl.gameState == "time-out")
        {
            activeInst = gameCtrl.activeInst.GetComponent<PlayerController>();
            nameTxt.text = activeInst.agentName.ToString();
        }
        if (gameCtrl.gameState == "live")
        {
            healthTxt.text = activeInst.health.ToString();
        }
        

    }
}
