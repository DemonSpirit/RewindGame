using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateAroundPoint : MonoBehaviour
{
    public float spd = 15f;
    [SerializeField] Transform RotatePos;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(RotatePos.position, Vector3.up, spd * Time.deltaTime);
    }
}
