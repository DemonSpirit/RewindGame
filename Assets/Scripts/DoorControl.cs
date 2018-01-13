using UnityEngine;
using System.Collections;

public class DoorControl : ActivatableObject
{
    
    [SerializeField] float openAmount = 3f;
    Vector3 startPos;
    public GameObject[] trigger = new GameObject[1];
    public ActivatableObject[] triggerCtrl = new ActivatableObject[1];

    //used for counting triggers
    [SerializeField] int ii = 0;
    
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        for (int i = 0; i < trigger.Length; i++)
        {
            triggerCtrl[i] = trigger[i].GetComponent<ActivatableObject>();
        }
        print("triggerctrl length" + triggerCtrl.Length.ToString());
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
        } else
        {
            on = startCondition;
        }

        if (on)
        {
            transform.position = startPos - (transform.right * openAmount);
            
        } else
        {
            
            transform.position = startPos;
        }
        
    }

    
}
