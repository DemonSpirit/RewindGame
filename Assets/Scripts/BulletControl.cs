using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float spd = 3f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += transform.forward * spd * Time.fixedDeltaTime;
	}
}
