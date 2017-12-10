using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour {

    GameControl gameCtrl;
    public GameObject[] segments;
    PlayerController activeInst;
    public GameObject segmentPrefab;
    public int segmentAmount = 50;
    int amtOfSegments = 0;
    float segmentPercentage,framePercentage;


    // Use this for initialization
    void Start () {
        gameCtrl = GameControl.main;
        segments = new GameObject[10];

    }
	
	// Update is called once per frame
	void Update () {

        switch (gameCtrl.gameState)
        {
            case "pre-live":
                activeInst = gameCtrl.activeInst.GetComponent<PlayerController>();
                amtOfSegments = activeInst.maxHealth / 50;
                segmentPercentage = 100 / amtOfSegments;
                framePercentage = segmentPercentage / 7f;

                for (int i = 0; i < amtOfSegments; i++)
                {
                    GameObject inst = Instantiate(segmentPrefab, new Vector3(0f + (50f * i), 0f, 0f), Quaternion.identity, transform);
                    segments[i] = inst;
                    HealthbarAnimator segmentCtrl = inst.GetComponent<HealthbarAnimator>();
                    segmentCtrl.segmentAmount = segmentAmount;

                }
                
                break;
            case "live":
                float healthPerc = activeInst.health / activeInst.maxHealth * 100;
                float draw = healthPerc / framePercentage;
                
                break;
            case "end":
                
                for (int i = 0; i < amtOfSegments; i++)
                {
                    Destroy(segments[i]);
                    segments[i] = null;
                    
                }
                
                break;
            default:
                break;
        }
        
        //

	}
}
