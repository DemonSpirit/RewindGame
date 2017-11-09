using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour {

    GameControl gameCtrl;
    public GameObject[] Segments;
    PlayerController activeInst;
    public GameObject segmentPrefab;
    int amtOfSegments = 0;
    public GameObject inst;

    // Use this for initialization
    void Start () {
        gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();
        for (int i = 0; i < 10; i++)
        {
            Segments[i] = null;
        }
	}
	
	// Update is called once per frame
	void Update () {

        switch (gameCtrl.gameState)
        {
            case "pre-live":
                activeInst = gameCtrl.activeInst.GetComponent<PlayerController>();
                amtOfSegments = activeInst.maxHealth / 50;

                for (int i = 0; i < amtOfSegments; i++)
                {
                    var inst = Instantiate(segmentPrefab, new Vector3(0f+(50f*i),0f,0f), Quaternion.identity,transform);
                    //Segments[i] = inst;
                    
                }
                
                break;
            case "live":
                
                break;
            case "end":
                /*
                for (int i = 0; i < amtOfSegments; i++)
                {
                    Destroy(Segments[i]);
                    Segments[i] = null;
                }
                */
                break;
            default:
                break;
        }
        
        //

	}
}
