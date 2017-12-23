using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColourForMatShade : MonoBehaviour {
    PlayerController playCtrl;
    [SerializeField]Material mat;

	// Use this for initialization
	void Update () {
        if (GameControl.main.gameState == "live")
        {
            playCtrl = transform.GetComponentInParent<PlayerController>();
            mat = GetComponent<Renderer>().material;
            
        }
        
        
	}
	
}
