using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public string state = "idle";
    float maxDist = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case "idle":
                LookForPlayer();
                break;
        }
    }

    void LookForPlayer()
    {
        RaycastHit hit;
        for (float i = -1f; i < 1 ; i += 0.25f)
        {
            for (int ii = 0; ii < length; ii++)
            {

            }
            Physics.Raycast(transform.position, new Vector3(i,0f,ii), out hit, maxDist);

        }
        
    }
}
