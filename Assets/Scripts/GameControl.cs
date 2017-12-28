using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
    // singleton
    public static GameControl main;
    public static Color redTeamColor = Color.red;
    public static Color blueTeamColor = Color.blue;
    public Image openScreen;

    #region Important Rewind Variables
    public int step = 0;
    public int endStep = 0;
    int stepsPerSecond = 60;
    public int layer = 0;
    public int maxLayer = 4;
    [SerializeField] int rewindSpd = 2;
    #endregion   
    // set time for the countdown timer.
    float countdownTime;
	[SerializeField] float countdownTimeLimit;
    public float time = 0;

    // set time limits for teams
	[SerializeField] float timeLimit;
    public float currentLayerTimeLimit;
    [SerializeField] float furthestLayerTimeLimit = 1f;
    public float timeLimitTeam1 = 4f;
    public float timeLimitTeam2 = 8f;

    // Game Mode and State
    public string gameMode = "versus";
    public string gameState = "pre-pick";
    int winner = 0;

    //Character Spawning
    public GameObject spawnPos;
    public GameObject activeInst;

    public int teamToSpawnFor = 0;

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
    public GameObject explosionPFX;
    #endregion

    public Text headerTxt;

    private void Awake()
    {
        main = this;
    }
    void Start () {
		time = timeLimit;
        gameState = "pre-pick";
        // pick phase
        pickCtrl = pickUI.GetComponent<PickUIController>();
        // start ambience
        AudioControl.Sound(AudioControl.instance.ambientRain);

        headerTxt = GameObject.Find("HeaderText").GetComponent<Text>();
    }

    
	void LateUpdate () {
        switch (gameMode)
        {
#region  solo game mode state
            case "solo":
                switch (gameState)
                {
                    case "live":

                        break;
                }
                break;
            #endregion
#region versus game mode state
            case "versus":
                switch (gameState)
                {
                    case "pre-pick":

                        openScreen.enabled = true;

                        /*
                        // enable the pick screen ui
                        pickUI.SetActive(true);        
                        */
                        // set vars for auto playback in background
                        time = furthestLayerTimeLimit;
                        endStep = Mathf.FloorToInt(stepsPerSecond * furthestLayerTimeLimit);
                        
                        step = 0;
                        ready = false;
                        // move counter up so it spawns for the next player
                        teamToSpawnFor++;
                        if (teamToSpawnFor > amountOfTeams) teamToSpawnFor = 1;
                        if (teamToSpawnFor == 1)
                        {
                            headerTxt.text = "Blue Player's turn: press enter to start.";
                        }
                        else
                        {
                            headerTxt.text = "Red Player's turn: press enter to start.";
                        }
                        headerTxt.alignment = TextAnchor.MiddleCenter;
                        headerTxt.text = "";
                        gameState = "pick";
                        break;
                    case "pick":

/*
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

    */
                        
                        // ready up
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            ready = true;
                        }
                        if (ready) gameState = "start";

                        // playback the game in the background
                        step++;
                        time -= Time.deltaTime;

                        // check if the loop is over and restart it.
                        if (step >= endStep)
                        {
                            time = furthestLayerTimeLimit;
                            step = 0;
                            GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
                            for (int i = 0; i < playerList.Length ; i++)
                            {
                                playerList[i].GetComponent<PlayerController>().Reset();
                            }
                        }

                        break;
                    case "start":
                        openScreen.enabled = false;
                        countdownTime = countdownTimeLimit;
                        pickUI.SetActive(false);
                        headerTxt.alignment = TextAnchor.MiddleCenter;

                        //Set the current team variables
                        SetCurrentTeam(teamToSpawnFor);
                        
                        // check if this is the furthest layer. this is to ensure when playing back actions, we get all the events instead of the events from the last layer's time limit.
                        if (currentLayerTimeLimit > furthestLayerTimeLimit) furthestLayerTimeLimit = currentLayerTimeLimit;

                        // spawn picked character
                        GameObject playerPrefab;
                        playerPrefab = characterArray[pick];
                        activeInst = Instantiate(playerPrefab, spawnPos.transform.position, spawnPos.transform.rotation);
                        pick = 0;
                        // set spawned character as the active instance
                        var instController = activeInst.GetComponent<PlayerController>();
                        instController.active = true;
                        instController.team = teamToSpawnFor;
                        headerTxt.text = "";
                        // reset game variables
                        NewLayer();
                        // Go to next gameState
                        gameState = "countdown";
						break;
					case "countdown": 
						arenaCam.SetActive(false);

                        if (countdownTime > 0)
                        {
                            countdownTime -= Time.deltaTime;
                            // Set time for the UI time text
                            time = countdownTime;
                        }
                        else
                        {
                            gameState = "pre-live";
                            if (teamToSpawnFor == 1) time = timeLimitTeam1;
                            if (teamToSpawnFor == 2) time = timeLimitTeam2;
                        }
	                    break;
                    case "pre-live":
                        
                        endStep = Mathf.FloorToInt(stepsPerSecond*currentLayerTimeLimit);
                        headerTxt.text = "";
                        gameState = "live";
                        break;
                    case "live":
						step++;
						time -= Time.deltaTime;						
						if (step >= endStep) gameState = "live-end";
                        break;
                    case "live-end":
                        gameState = "pre-rewind";
                        break;
                    case "pre-rewind":
                        // - Turn off input for the active character.
                        activeInst.GetComponent<PlayerController>().active = false;
                        // set time to count from zero
                        time = 0f;
                        step--;
                        headerTxt.text = "Rewinding time";
                        gameState = "rewind";
                        //step = Mathf.FloorToInt(furthestLayerTimeLimit * stepsPerSecond);
                        
                        break;

                    case "rewind":
                        // Rewind the game events to convey to the player that we are going back in time.
                        // rewind steps
                        
                        step -= rewindSpd;
						time += rewindSpd * Time.deltaTime;
                        // - Check if back to the start
						if (step <= 0) gameState = "rewind-end";
                        break;

                    case "rewind-end":                 
                        // turn off camera and other components for active inst
                        activeInst.GetComponent<PlayerController>().DestroyComponentsAtLayerEnd();
                        // Turn arena cam on for the pick/gameover phase
                        arenaCam.SetActive(true);
                        gameState = "end";
                        break;
                        
                    case "playback":
                        
                        break;

                    case "end":

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
                            if (winner == 1)
                            {
                                Debug.Log("Blue wins!");
                                headerTxt.text = "Blue player wins!";
                            }
                            if (winner == 2)
                            {
                                Debug.Log("Red wins!");
                                headerTxt.text = "Red player wins!";
                            }

                            Debug.Log("Blue: " + team1points.ToString() + " : Red: " + team2points.ToString());
                            
                            gameState = "gameover";
                        } else
                        { // game is not over yet, go to pick phase.
                            gameState = "pre-pick";                  
                        }
                        break;
                    case "gameover":              
                        //
                        Debug.Log("Game Over");
                        break;
                }
                break;
        }
#endregion
    }
    void SetCurrentTeam(int team)
    {
        if (team == 1)
        {
            spawnPos = GameObject.Find("BluePlayerSpawn");
            currentLayerTimeLimit = timeLimitTeam1;
        }
        if (team == 2)
        {
            spawnPos = GameObject.Find("RedPlayerSpawn");
            currentLayerTimeLimit = timeLimitTeam2;
        }
    }
    void NewLayer()
    {
        
        team1points = 0;
        team2points = 0;
        timeLimitTeam1 += 2f;
        timeLimitTeam2 += 2f;
        step = 0;
        layer++;
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 50, 50, 50), "projectiles: "+GameObject.FindGameObjectsWithTag("Projectile").Length.ToString());
        
    }
}
