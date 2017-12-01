using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZoneController: MonoBehaviour {

    public int team = 0;
    public float TimeX = 0.0f;
    Renderer rend;
    Color color;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        if (team == 1) {
            color = Color.blue;
        } else
        {
            color = Color.red;
        }
        rend.material.color = color;


	}
	
	// Update is called once per frame
	void Update () {
        rend.material.SetFloat("_TimeX", TimeX);
	}
    
}
