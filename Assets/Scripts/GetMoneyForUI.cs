using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetMoneyForUI : MonoBehaviour
{
    Text txt;
    GameControl gameCtrl;
    public int team = 0;
    // Use this for initialization
    void Start()
    {
        txt = GetComponent<Text>();
        gameCtrl = GameControl.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (team == 1) txt.text = gameCtrl.team1money.ToString()+"$";
        if (team == 2) txt.text = gameCtrl.team2money.ToString()+"$";
    }
}
