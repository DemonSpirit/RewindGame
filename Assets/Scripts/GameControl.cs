using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    
    // Rewind Variables
    public int maxSteps = 900;
    public float[,] inputArray = new float[900, 4];
    public int step = 0;
    public int layer = 0;
    public int maxLayer = 4;
    public int time = 0;

    // Game Mode and State
    public string gameMode = "versus";
    public string gameState = "pre-pick";
    int winner = 0;

    //Character Spawning
    public GameObject spawnPos;
    public GameObject activeInst;

    public int teamToSpawnFor = 1;

    #region Versus Mode Variables
    //Team Variables
    public int amountOfTeams = 2;
    public int team1points = 0;
    public int team2points = 0;
    public int team1money = 0;
    public int team2money = 0;

    // Ref Arenacam - Spinning camera at the end of a layer.
    public GameObject arenaCam;

    #endregion

    // PICK SCREEN //
    // Ref Character Pick UI
    public GameObject pickUI;
    public GameObject[] characterArray;
    PickUIController pickCtrl;
    bool ready = false;
    public int pick = 0;
    int amtOfCharacters = 3;

    float secondTimer = 0;

    #region Sound References
    [FMODUnity.EventRef]
    public string changeSelectionSFX;
    [FMODUnity.EventRef]
    public string confirmSelectionSFX;
    // BGM
    [FMODUnity.EventRef]
    public string characterSelectBGM;
    [FMODUnity.EventRef]
    public string noMoneySFX;
    #endregion
    #region GameObject References
    public GameObject playerHitPFX;
    public GameObject woodHitPFX;
    #endregion

    void Start () {
        gameState = "pre-pick";
        // pick phase
        pickCtrl = pickUI.GetComponent<PickUIController>();
    }

    
	void LateUpdate () {
        switch (gameMode)
        {
            case "solo":
                switch (gameState)
                {
                    case "live":

                        break;
                }
                break;
            case "versus":
                switch (gameState)
                {
                    case "pre-pick":
                        pickUI.SetActive(true);              
                        gameState = "pick";
                        break;
                    case "pick":

                        // choose hero
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            pick++;
                            // play sound
                            FMODUnity.RuntimeManager.PlayOneShot(changeSelectionSFX);
                        }
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            pick--;
                            // play sound
                            FMODUnity.RuntimeManager.PlayOneShot(changeSelectionSFX);
                        }
                        // pick clamp
                        if (pick > amtOfCharacters-1) pick = 0;
                        if (pick < 0) pick = (amtOfCharacters-1);
                        // change pick UI
                        pickCtrl.ChangePickUI(pick);
                        // confirm pick
                        if (Input.GetKeyDown(KeyCode.Return))
                        {

                            switch (pick)
                            {
                                case 0:
                                    ready = true;
                                    break;
                                case 1:
                                    if (teamToSpawnFor == 1 && team1money >= 100)
                                    {
                                        team1money -= 100;
                                        ready = true;
                                    } else
                                    {
                                        FMODUnity.RuntimeManager.PlayOneShot(noMoneySFX);
                                    }
                                    if (teamToSpawnFor == 2 && team2money >= 100)
                                    {
                                        team2money -= 100;
                                        ready = true;
                                    }
                                    else
                                    {
                                        FMODUnity.RuntimeManager.PlayOneShot(noMoneySFX);
                                    }
                                    break;
                                case 2:
                                    if (teamToSpawnFor == 1 && team1money >= 200)
                                    {
                                        team1money -= 200;
                                        ready = true;
                                    }
                                    else
                                    {
                                        FMODUnity.RuntimeManager.PlayOneShot(noMoneySFX);
                                    }
                                    if (teamToSpawnFor == 2 && team2money >= 200)
                                    {
                                        team2money -= 200;
                                        ready = true;
                                    }
                                    else
                                    {
                                        FMODUnity.RuntimeManager.PlayOneShot(noMoneySFX);
                                    }
                                    break;
                       
                            }
                    
                            // play sound
                            FMODUnity.RuntimeManager.PlayOneShot(confirmSelectionSFX);
                        }
                        // ready up
                        if (ready) gameState = "start";

                        break;
                    case "start":
                        pickUI.SetActive(false);
                        if (teamToSpawnFor == 1) spawnPos = GameObject.Find("BluePlayerSpawn");
                        if (teamToSpawnFor == 2) spawnPos = GameObject.Find("RedPlayerSpawn");

                        // spawn picked character
                        GameObject playerPrefab;
                        playerPrefab = characterArray[pick];
                        activeInst = Instantiate(playerPrefab, spawnPos.transform.position, spawnPos.transform.rotation);
                        pick = 0;
                        // set spawned character as the active instance
                        var instController = activeInst.GetComponent<PlayerController>();
                        instController.active = true;
                        instController.team = teamToSpawnFor;

                        // reset game variables
                        team1points = 0;
                        team2points = 0;
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
                        secondTimer += Time.deltaTime;
                        if (secondTimer >= 1) step++;
                        time = (step / 60);
                        if (step >= maxSteps) gameState = "pre-rewind";
                        break;
                    case "pre-rewind":
                        // - Turn off input for the active character.
                        activeInst.GetComponent<PlayerController>().active = false;
                        gameState = "rewind";
                        step--;
                        break;

                    case "rewind":
                        // Rewind the game events to convey to the player that we are going back in time.
                        // rewind steps
                        Debug.Log("Rewinding Step: " + step.ToString());
                        step-=2;
                        time = (step / 60);
                        // - Check if back to first step.
                        if (step <= 0) gameState = "end";
                        break;
                    case "playback":
                        // this state playback the game.

                        break;

                    case "end":
                
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
                            gameState = "pre-pick";
                            // move counter up so it spawns for the next player
                            teamToSpawnFor++;
                            if (teamToSpawnFor > amountOfTeams) teamToSpawnFor = 1;
                        }
                        break;
                    case "gameover":              
                        //
                        Debug.Log("Game Over");
                        break;
                }
                break;
        }       
    }
}
