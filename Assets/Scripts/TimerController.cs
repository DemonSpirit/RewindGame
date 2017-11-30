using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

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

		if (time > 3)
		{
			time = decimal.Round(time, 0);
		}
		else 
		{
			time = decimal.Round(time, 1);
		}

		txt.text = time.ToString();
	}
}
