using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float spd = 3f;
    public int dmg = 10;
    public int points = 10;
    public int team;
    public string type = "";
    //PlayerController ctrl;
    public GameControl gameCtrl;
    public GameObject playerHitFX;
    public GameObject blockHitFX;
    PlayerController otherCtrl;

	// Use this for initialization
	void Start () {
        gameCtrl = GameControl.main;
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
        switch (other.tag)
        {
            case "BlockProjectiles":
                otherCtrl = other.GetComponentInParent<PlayerController>();
                if (otherCtrl.alive == true && otherCtrl.team != team )
                {
                    Debug.Log("Proj Blocked");
                    spawnParticle(blockHitFX);
                    DestroyBullet();
                }
                break;
            case "Player":
                
                var ctrl = other.GetComponent<PlayerController>();
                var gmCtrl = GameObject.Find("GameController").GetComponent<GameControl>();

                if (type == "healBullet" && ctrl.team == team && ctrl.alive == false)
                {
                    Debug.Log(ctrl.name + " Revived!");
                    ctrl.health = ctrl.maxHealth;
                    DestroyBullet();
                }

                if (ctrl.team != team && ctrl.alive == true && ctrl.active == false)
                {
                     ctrl.health -= dmg;
                    // particle effect
                    spawnParticle(playerHitFX);
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
    void spawnParticle(GameObject particle)
    {
        GameObject inst = Instantiate(particle, transform.position, transform.rotation);
        GameObject.Destroy(inst, 1.5f);
    }
}
