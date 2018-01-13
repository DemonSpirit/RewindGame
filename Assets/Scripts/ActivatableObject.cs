using UnityEngine;
using System.Collections;

public abstract class ActivatableObject : MonoBehaviour
{
    public bool on;
    public float timeout = 0f;
    public float timeoutTime = 1f;
    public bool startCondition;
    Vector3 startPosition;
    Quaternion startRotation;

    private void Awake()
    {
        startCondition = on;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (SoloGameController.main.gameState == "reset")
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            
        }
    }
}
