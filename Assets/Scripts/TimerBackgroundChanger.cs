using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerBackgroundChanger : MonoBehaviour
{
    GameControl gameCtrl;
    Image image;

    public Sprite redTimer;
    public Sprite blueTimer;

    // Use this for initialization
    void Start()
    {
        gameCtrl = GameControl.main;
        image = GetComponent<Image>();
    }

    void Update()
	{   if (gameCtrl.gameState == "pre-live" || gameCtrl.gameState == "countdown")
        {
            ChangeTimerBackground(gameCtrl.teamToSpawnFor);
        }
    }

    public void ChangeTimerBackground(int teamNumber)
    {
        
        if (teamNumber == 2)
        {
            image.sprite = redTimer;
        } else
        {
            image.sprite = blueTimer;
        }
        
        
    }
}
