using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Speeding : MonoBehaviour {

    Transform xform;
    Vector3 lastPosition;
    Vector3 currPosition;
    Vector3 diffPosition;

    public Animator animator;
    public Transform gunXform;
    public float speed;
    public float forwardsAmount;

    private void Update()
    {


        currPosition = xform.position;
        currPosition.y = 0;


        Vector3 direction = gunXform.forward;
        direction.y = 0;
        direction.Normalize();

        diffPosition = currPosition - lastPosition;
        speed = diffPosition.magnitude / Time.deltaTime;

        forwardsAmount = Vector3.Dot(direction, diffPosition.normalized);

        lastPosition = currPosition;

        //Debug.Log(forwardsAmount + "," + speed);

        animator.SetFloat("Speed", speed);

    }

    private void OnEnable()
    {
        xform = this.transform;
        lastPosition = xform.position;
    }
}
