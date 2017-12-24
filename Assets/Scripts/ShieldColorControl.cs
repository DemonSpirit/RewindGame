using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldColorControl : MonoBehaviour {

    [SerializeField] GameObject redShield;
    [SerializeField] GameObject blueShield;
    PlayerController parentPlayer;
	// Use this for initialization
	void Start () {

        parentPlayer = GetComponentInParent<PlayerController>();
        redShield = transform.GetChild(0).gameObject;
        blueShield = transform.GetChild(1).gameObject;

        
	}

    private void Update()
    {
        if (GameControl.main.gameState == "pre-live")
        {
            if (parentPlayer.team == 2)
            {
                redShield.SetActive(true);
            }
            else
            {
                blueShield.SetActive(true);
            }
        }
    }


}
