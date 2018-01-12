using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoloGameController : MonoBehaviour {

    public static SoloGameController main;
    public string gameState = "live";
    public float playbackSpeed = 1f;
    public Text textBox;
    
    // Use this for initialization
    private void Awake()
    {
        main = this;

    }

    // Update is called once per frame
    void LateUpdate () {
        switch (gameState)
        {
            case "level-complete":
                
                break;
            
        }
    }
}
