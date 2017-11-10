using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateAroundPoint : MonoBehaviour
{
    public float spd = 15f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, spd * Time.deltaTime);
    }
}
