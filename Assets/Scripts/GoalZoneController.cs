using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZoneController: MonoBehaviour {

    public int team = 0;
    public float TimeX = 0.0f;
    public int maxHealth = 1000;
    public int health;
    Renderer rend;
    Color color;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        if (team == 1) {
            color = Color.blue;
        } else
        {
            color = Color.red;
        }
        rend.material.color = color;

        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        rend.material.SetFloat("_TimeX", TimeX);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(10);
            print(health);
        }
	}

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            GameObject inst = Instantiate(GameControl.main.bitPrefab, transform.position, transform.rotation);

            inst.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 10f;
        }
    }
        
    
}
