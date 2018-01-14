using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlateControl : ActivatableObject
{
    
    Vector3 startPos;
    [SerializeField] int openAtKeyStage = 0;

    // Use this for initialization
    void Start() {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        
        if (SceneManager.GetActiveScene().name != "Hub")
        {
            if (on)
            {
                transform.position = startPos - new Vector3(0, +0.2f, 0);
            }
            else
            {
                transform.position = startPos;
            }
        } else
        {
            if (SoloGameController.main.keys == openAtKeyStage)
            {
                if (on)
                {
                    transform.position = startPos - new Vector3(0, +0.2f, 0);
                }
                else
                {
                    transform.position = startPos;
                }
            }
            
        }
        

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "echo")
        {
            if (SceneManager.GetActiveScene().name == "Hub")
            {   if (SoloGameController.main.keys == openAtKeyStage)
                {
                    on = true;
                }
                
            } else
            {
                on = true;
            }
                
        }
            
    }

    private void LateUpdate()
    {
        on = false;
    }


}
