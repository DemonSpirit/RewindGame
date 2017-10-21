using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCollider : MonoBehaviour {

    public EnemyController owner;
    private void Awake()
    {
        owner = GetComponentInParent<EnemyController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (owner.state == "idle")
        {
            Debug.Log(other.name + " entered trigger!");
            owner.target = other.transform;
            owner.state = "alert";
        }
    }
}
