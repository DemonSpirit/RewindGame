using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

	int startFontSize;
    public Text txt;
    public GameControl gameControl;
	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        gameControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateText();
	}

	void UpdateText()
	{
		decimal time = (decimal) gameControl.time;

		switch (gameControl.gameState)
		{
			case "countdown":
				time = decimal.Round(time, 0);
				txt.color = Color.yellow;
				break;
            
			case "live":
			case "pre-rewind":
			case "rewind":

				if (time > 3)
				{
					time = decimal.Round(time, 0);
					txt.color = Color.white;
				}

				else 
				{
				time = decimal.Round(time, 1);
				txt.color = Color.red;
				}
			break;

		default:
			txt.text = "";
			return;
		}
			
		txt.text = time.ToString();

		if (time < 0)
			txt.text = "0";
	}
}
