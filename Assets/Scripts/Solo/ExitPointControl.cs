using UnityEngine;
using System.Collections;

public class ExitPointControl : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoloGameController.main.gameState = "level-complete";
        }
    }
}
