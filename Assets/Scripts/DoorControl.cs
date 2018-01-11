using UnityEngine;
using System.Collections;

public class DoorControl : ActivatableObject
{
    public bool open = false;
    [SerializeField] float openAmount = 3f;
    Vector3 startPos;
    public GameObject trigger;
    ActivatableObject triggerCtrl;
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        triggerCtrl = trigger.GetComponent<ActivatableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (triggerCtrl.on)
        {
            transform.position = startPos - new Vector3(openAmount, 0f, 0f);
        } else
        {
            transform.position = startPos;
        }
    }
}
