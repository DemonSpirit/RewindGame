using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float spd = 3f;
    public int dmg = 10;
    public int points = 10;
    public int team;
    //PlayerController ctrl;
    public GameControl gameCtrl;
    public GameObject particlePrefab;

	// Use this for initialization
	void Start () {
        gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();
        Destroy(gameObject, 5f);
	}

    // Update is called once per frame
    void Update()
    {
        if (gameCtrl.gameState == "start") Destroy(gameObject);
    }
    void FixedUpdate () {
        
        transform.position += transform.forward * spd * Time.fixedDeltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        // particle effect
        GameObject inst = Instantiate(particlePrefab,transform.position,transform.rotation);
        GameObject.Destroy(inst, 1.5f);

        switch (other.tag)
        {   
            case "Player":
                
                var ctrl = other.GetComponent<PlayerController>();
                var gmCtrl = GameObject.Find("GameController").GetComponent<GameControl>();
                if (ctrl.team != team && ctrl.alive == true)
                {
                    if (ctrl.team != team)
                    {
                        ctrl.health -= dmg;
                    }

                    DestroyBullet();

                }
                break;
            case "Scorezone":
                

                if (other.GetComponent<GoalZoneController>().team == 1 && team != 1)
                {
                    gameCtrl.team2points += points;
                }
                if (other.GetComponent<GoalZoneController>().team == 2 && team != 2)
                {
                    gameCtrl.team1points += points;
                }
                DestroyBullet();
                break;
        }
    }
    void DestroyBullet()
    {
        
        Destroy(gameObject);
    }
}
