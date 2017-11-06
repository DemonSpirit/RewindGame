using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {
    public float rotateSpd = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpd);
        transform.Rotate(Vector3.right * Time.deltaTime * rotateSpd);
        ;	}
}
