using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GetScoreForUI : MonoBehaviour
{
    GameControl gameCtrl;
    Text txt;
    public int team = 0;
    // Use this for initialization
    void Start()
    {
        gameCtrl = GameControl.main;
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (team == 1) txt.text = gameCtrl.team1points.ToString();
        if (team == 2) txt.text = gameCtrl.team2points.ToString();
    }
}
