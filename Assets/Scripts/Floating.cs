using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {
    public float moveSpeed = 5f;
    public float frequency = 20.0f; // speed of sine movement
    public float magnitude = 0.5f; // size of sine movement
    Vector3 axis;
    Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = transform.position;
        
        axis = transform.up;
	}
	
	// Update is called once per frame
	void Update () {
        //pos += transform.up * Time.deltaTime * moveSpeed;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
	}
}
