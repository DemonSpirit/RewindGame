using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public int maxSteps = 900;
    public int step = 0;
    public int layer = 0;
    public int time = 0;
    public string gameState = "start";
    public float[,] inputArray = new float[900 , 4];
    public GameObject playerPrefab;
    public Vector3 spawnPos;
    public GameObject activeInst;

	// Use this for initialization
	void Start () {
        gameState = "start";
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        switch (gameState)
        {
            case "start":
                activeInst = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
                activeInst.GetComponent<PlayerController>().active = true;
                step = 0;
                layer++;
                gameState = "live";
                
                break;
            case "live":
                step++;
                time = (step / 60);
                if (step >= maxSteps) gameState = "end";
                break;

            case "playback":
                break;

            case "end":
                activeInst.GetComponent<PlayerController>().active = false;
                activeInst.GetComponent<PlayerController>().DestroyComponentsAtLayerEnd();
                
                gameState = "start";
                break;

        }
        
    }
}
