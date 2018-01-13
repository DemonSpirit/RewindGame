using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 moveDir = Vector3.forward;
    Vector3 targetPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] float moveAmount = 3f;
    public float moveSpd = 0.3f;
    float counter = 0f;
    [SerializeField] float directionChangeTime = 1f;
    bool forwards = true;
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + (moveDir * moveAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (forwards)
        {
            targetPos = endPos; 
           
        } else
        {
            targetPos = startPos;
        }

        transform.position = Vector3.Slerp(transform.position, targetPos, moveSpd);
        counter += Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPos) <= 1f)
        {
            forwards = !forwards;
            counter = 0f;
        }

    }

    
}
