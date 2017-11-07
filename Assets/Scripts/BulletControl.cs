using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float spd = 3f;
    public int dmg = 10;
    public int points = 10;
    public int team;
    //PlayerController ctrl;
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
        switch (other.tag)
        {
            case "Player":
                var ctrl = other.GetComponent<PlayerController>();
                if (ctrl.team != team && ctrl.alive == true)
                {
                    ctrl.health -= dmg;
                    if (team == 1) gameCtrl.team1points += points;
                    if (team == 2) gameCtrl.team2points += points;

                    Destroy(gameObject);

                }
                break;
            default:
                break;
        }
    }
}
