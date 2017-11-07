using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public string state = "idle";
    float maxDist = 10f;
    float turnSpeed = 0.1f;
    public Transform target;
    public GameControl gameControl;
    public int dmg = 100;

    public float fireRate = 1f;
    public float nextFireTime = 0f;

    public int maxHealth = 100;
    public int health;

    Vector3 startPos;
    Quaternion startRot;
    public float shootDist = 10f;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startRot = transform.rotation;
        health = maxHealth;
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
                            // I do this because hit.transform will give me the cone collider not the parent collider.
                            if (hit.transform == target && (Time.time >= nextFireTime))
                            {
                                var targetController = target.gameObject.GetComponent<PlayerController>();
                                if (targetController.alive == true)
                                {
                                    targetController.health -= dmg;
                                    nextFireTime = Time.time + fireRate;
                                    
                                    target = null;
                                    state = "idle";                                    
                                }
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
