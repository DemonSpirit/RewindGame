using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoloGameController : MonoBehaviour {

    public static SoloGameController main;
    
    public string gameState = "live";
    public int keys = 0;
    public float playbackSpeed = 1f;
    public Text textBox;
    int counter = 0;
    [SerializeField] string[] level = new string[3];
    public float narrativeStage = 0f;
    
    // Use this for initialization
    private void Awake()
    {
        main = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("master-solo-game-controller"))
        {
            if (gameObject.tag == "GameController")
            {
                Destroy(gameObject);
            }
        }

        textBox = GameObject.Find("DialogueText").GetComponent<Text>();
    }

    // Update is called once per frame
    void LateUpdate () {
        
        switch (gameState)
        {
            case "reset":
                counter++;
                if (counter >= 1 )
                {
                    gameState = "live";
                    counter = 0;
                }
                break;

            case "level-complete":
                SceneManager.LoadScene(level[1],LoadSceneMode.Single);
                break;
            
        }
    }
}
