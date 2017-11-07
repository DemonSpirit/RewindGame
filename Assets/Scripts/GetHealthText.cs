using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GetHealthText : MonoBehaviour
{
    public Text txt;
    public GameObject activeInst;
    public GameControl gameCtrl;

    // Use this for initialization
    void Start()
    {
        gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();
        txt = GetComponent<Text>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (gameCtrl.gameState == "live")
        {
            activeInst = gameCtrl.activeInst;
            txt.text = activeInst.GetComponent<PlayerController>().health.ToString();
        }
        

    }
}
