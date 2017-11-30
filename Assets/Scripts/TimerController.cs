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
		int time = (int) gameControl.time;
        txt.text = time.ToString();
	}
}
