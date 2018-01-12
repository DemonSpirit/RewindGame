using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateControl : ActivatableObject
{
    
    Vector3 startPos;

    // Use this for initialization
    void Start() {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        print(on);
        if (on)
        {
            transform.position = startPos - new Vector3(0, +0.2f, 0);
        } else
        {
            transform.position = startPos;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "echo")
        {
            
            on = true;
        }
            
    }

    private void LateUpdate()
    {
        on = false;
    }


}
