using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public int maxSteps = 900;
    public int step = 0;
    public int layer = 0;
    public int maxLayer = 4;
    public int time = 0;
    public string gameState = "pick";
    public float[,] inputArray = new float[900 , 4];
    public GameObject spawnPos;
    public GameObject activeInst;

    int teamToSpawnFor = 1;
    public int amountOfTeams = 2;
    public int team1points = 0;
    public int team2points = 0;

    // Ref Arenacam
    public GameObject arenaCam;


    // PICK SCREEN //
    public GameObject[] characterArray;
    bool ready = false;
    int pick = 0;
    //
    int winner = 0;

	// Use this for initialization
	void Start () {
        gameState = "pick";
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        switch (gameState)
        {
            case "pick":

                // choose hero
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    pick++;
                    Debug.Log(pick);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    pick--;
                    Debug.Log(pick);
                }
                // pick clamp
                if (pick > 3) pick = 0;
                if (pick < 0) pick = 3;
                // confirm pick
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    ready = true;
                }
                // ready up
                if (ready) gameState = "start";

                break;
            case "start":
                spawnPos = GameObject.Find("PlayerSpawn");

                // spawn picked character
                GameObject playerPrefab;
                playerPrefab = characterArray[pick];
                activeInst = Instantiate(playerPrefab, spawnPos.transform.position, Quaternion.identity);
                pick = 0;
                // set spawned character as the active instance
                var instController = activeInst.GetComponent<PlayerController>();
                instController.active = true;
                instController.team = teamToSpawnFor;
                // move counter up so it spawns for the next player
                teamToSpawnFor++;
                if (teamToSpawnFor > amountOfTeams) teamToSpawnFor = 1;

                // reset game variables
                ready = false;
                step = 0;
                layer++;
                gameState = "pre-live";
                
                break;
            case "pre-live":
                arenaCam.SetActive(false);
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
                // Turn arena cam on for the pick/gameover phase
                arenaCam.SetActive(true);
                //

                // check if the game is over.
                if (layer == maxLayer)
                {
                    if (team1points > team2points)
                    {
                        winner = 1;
                    } else
                    {
                        winner = 2;
                    }
                    if (winner == 1) Debug.Log("Blue wins!");
                    if (winner == 2) Debug.Log("Red wins!");

                    Debug.Log("Blue: " + team1points.ToString() + " : Red: " + team2points.ToString());
                    gameState = "gameover";
                } else
                {
                    gameState = "pick";
                }
                
                break;
            case "gameover":              
                //
                Debug.Log("Game Over");
                break;

        }
        
    }
}
