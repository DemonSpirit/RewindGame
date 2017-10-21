using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public string state = "idle";
    float maxDist = 10f;
    float turnSpeed = 0.1f;
    public Transform target;
    public GameControl gameControl;

    Vector3 startPos;
    Quaternion startRot;
    public float shootDist = 10f;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameControl.gameState)
        {
            case "start":
                transform.position = startPos;
                transform.rotation = startRot;

                break;
            case "live":
                switch (state)
                {
                    case "idle":
                        //LookForPlayer();
                        break;
                    case "alert":
                        LookAtTarget();
                        Vector3 dir = target.position - transform.position;
                        Debug.DrawRay(transform.position, dir, Color.green);

                        RaycastHit hit;
                        if (Physics.Raycast(transform.position,dir,out hit, shootDist))
                        {
                            Debug.Log(hit.transform.name);
                            
                            // I do this because hit.transform will give me the cone collider not the parent collider.
                            if (hit.transform == target)
                            {
                                //Target is in range
                                Debug.Log("Target is in range");
                                
                            }

                        }
                        break;
                }
                break;
            case "end":
                state = "idle";
                target = null;
                
                break;
        }
        
    }

    void LookAtTarget()
    {
        Quaternion toRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.time * turnSpeed);
        
    }
}
