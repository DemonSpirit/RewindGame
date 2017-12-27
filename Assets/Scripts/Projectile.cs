using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    Rigidbody rb;
    public float moveSpd = 500f;
    [SerializeField] float startSpd = 4000f;
    public Transform target;
    float rotAmt = 10f;
    public int team = 0;
    [SerializeField] int pointsGain = 1;
    Collider col;
    
    public bool isPickup = false;
    public bool real = false; // controls wether this is a real projectile or just used for replay.
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        target = GameObject.Find("BlueGoal").transform;
        
        rb.AddForce(transform.forward * startSpd);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation, rotAmt);
        if (!isPickup) rb.AddForce(transform.forward*moveSpd);
        if (GameControl.main.gameState != "live" && real == true) real = false;
        if (GameControl.main.gameState == "pre-live" && real == false) Destroy(gameObject);
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (real)
        {
            if (other.tag == "Scorezone")
            {
                if (GameControl.main.gameState == "live" && other.GetComponent<GoalZoneController>().team != team)
                {
                    if (team == 1) GameControl.main.team1points += pointsGain;
                    if (team == 2) GameControl.main.team2points += pointsGain;
                }
                Explode();


            }

            if (other.tag == "Projectile" && other.GetComponent<Projectile>().team != team)
            {
                
                Explode();

            }

            if (other.tag == "BlockProjectiles")
            {
                StopVelocity();
                FMODUnity.RuntimeManager.PlayOneShot(AudioControl.instance.blockSFX, transform.position);

            }

            if (other.tag == "Player" && isPickup)
            {
                PlayerController colPlayer = other.gameObject.GetComponent<PlayerController>();
                if (colPlayer.ammo < colPlayer.maxAmmo) colPlayer.ammo++;
                Destroy(gameObject);
                print("ammo picked up");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopVelocity();
        print("hit");
        isPickup = true;


        if (collision.transform.tag == "Player")
        {
            print("player collision");
            PlayerController colPlayer = collision.transform.GetComponent<PlayerController>();
            if (colPlayer.team != team)
            {
                colPlayer.health = 0;
                StopVelocity();
                FMODUnity.RuntimeManager.PlayOneShot(AudioControl.instance.painHitSFX,transform.position);
            } else
            {   if (isPickup)
                {
                    
                    if (colPlayer.ammo < colPlayer.maxAmmo) colPlayer.ammo++;
                    Destroy(gameObject);
                    print("ammo picked up");
                }

            }
        }
        
    }

    void StopVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
        isPickup = true;
    }
    void Explode()
    {
        GameObject inst = Instantiate(GameControl.main.explosionPFX, transform.position, transform.rotation);
        Destroy(inst, 2f);
        Destroy(gameObject);
    }
}
