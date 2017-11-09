using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCollider : MonoBehaviour {

    public EnemyController owner;
   
    void Start()
    {
        owner = GetComponentInParent<EnemyController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (owner.state == "idle" && other.GetComponent<PlayerController>().alive == true)
        {
            
            owner.target = other.transform;
            owner.state = "alert";
        }
    }
}
