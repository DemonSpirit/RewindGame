using UnityEngine;
using System.Collections;

public class LaserControl : ActivatableObject
{
    float startDist = 15f;
    public float distance = 15f;
    public bool canKill = false;
    LineRenderer lineRend;
    [SerializeField] int ii = 0;

    public GameObject[] trigger = new GameObject[1];
    public ActivatableObject[] triggerCtrl = new ActivatableObject[1];
    

    // Use this for initialization
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        startCondition = on;
        startDist = distance;

        for (int i = 0; i < trigger.Length; i++)
        {
            triggerCtrl[i] = trigger[i].GetComponent<ActivatableObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        ii = 0;
        for (int i = 0; i < triggerCtrl.Length; i++)
        {
            if (triggerCtrl[i].on == true)
            {
                ii++;
            }
        }
        
        if (ii >= triggerCtrl.Length)
        {
            on = !startCondition;

        }
        else
        {

            on = startCondition;

        }

        if (on)
        {
            lineRend.enabled = true;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                if (hit.collider.tag == "Player" || hit.collider.tag == "echo") ;
                {
                    distance = Vector3.Distance(transform.position, hit.transform.position);
                    lineRend.SetPosition(1, Vector3.forward * distance);
                    if (canKill && hit.collider.tag == "Player")
                    {
                        SoloPlayerController.main.Die();
                    }
                    
                }
                if (hit.collider.tag == "lasercatcher")
                {
                    ActivatableObject catchCtrl = hit.collider.GetComponent<ActivatableObject>();
                    catchCtrl.on = true;
                    catchCtrl.timeout = catchCtrl.timeoutTime;
                    distance = Vector3.Distance(transform.position, hit.transform.position);
                    lineRend.SetPosition(1, Vector3.forward * distance);
                }
                /*
                if (hit.collider.tag == "lasersource")
                {
                    ActivatableObject laserCtrl = hit.collider.GetComponent<ActivatableObject>();
                    laserCtrl.on = !laserCtrl.startCondition;
                }
                */
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
