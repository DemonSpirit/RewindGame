using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointControl : MonoBehaviour {

    public int checkpointNumber;
    SoloPlayerController playCtrl;

    private void Start()
    {
        playCtrl = SoloPlayerController.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            
            if (playCtrl.checkpoint == null)
            {
                SetCheckpoint();
                
            }
            if (playCtrl.checkpoint.checkpointNumber < checkpointNumber)
            {
                SetCheckpoint();
            }
        }
    }

    void SetCheckpoint()
    {
        playCtrl.checkpoint = this;
        
    }
}
