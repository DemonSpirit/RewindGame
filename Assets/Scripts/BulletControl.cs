using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float spd = 3f;
    public int dmg = 10;
    public int team;
    PlayerController ctrl;
    GameControl gameCtrl;

	// Use this for initialization
	void Start () {
        gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();
        Destroy(gameObject, 5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gameCtrl.gameState == "start") Destroy(gameObject);
        transform.position += transform.forward * spd * Time.fixedDeltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player")
        { ctrl = other.GetComponent<PlayerController>();
            if (ctrl.team != team)
            {
                ctrl.health -= dmg;
                Destroy(gameObject);
                Debug.Log("Bullet Destroyed");

            }
        }
    }
}
