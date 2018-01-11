using UnityEngine;
using System.Collections;

public class LaserControl : ActivatableObject
{
    public float startDist = 15f;
    public float distance = 15f;
    LineRenderer lineRend;
    // Use this for initialization
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (on)
        {
            lineRend.enabled = true;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                if (hit.collider.tag == "Player")
                {
                    distance = Vector3.Distance(transform.position, hit.transform.position);
                    lineRend.SetPosition(1, Vector3.forward * distance);
                }
                if (hit.collider.tag == "lasercatcher")
                {
                    ActivatableObject catchCtrl = hit.collider.GetComponent<ActivatableObject>();
                    catchCtrl.on = true;
                    catchCtrl.timeout = catchCtrl.timeoutTime;
                    distance = Vector3.Distance(transform.position, hit.transform.position);
                    lineRend.SetPosition(1, Vector3.forward * distance);
                }
            }
            else
            {
                distance = startDist;
                lineRend.SetPosition(1, Vector3.forward * distance);

            }
        } else
        {
            lineRend.enabled = false;
        }
        

        
        

        

    }
}
