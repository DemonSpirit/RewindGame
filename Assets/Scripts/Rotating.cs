using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {
    public float rotateSpd = 5f;
    [SerializeField] Vector3 axis = Vector3.up;
	
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(axis * Time.deltaTime * rotateSpd);
      
     }
}
