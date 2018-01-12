using UnityEngine;
using System.Collections;

public class TimerSwitch : ActivatableObject
{
    public int powerSourcesRequired = 2;
    
    
    public GameObject[] powerSources = new GameObject[2];
    ActivatableObject[] sourceCtrl = new ActivatableObject[2];

    

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < powerSources.Length; i++)
        {
            sourceCtrl[i] = powerSources[i].GetComponent<ActivatableObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        
        if (on)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);

            timeout += Time.deltaTime;
            if (timeout >= timeoutTime) on = false;
        } else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);

            if (sourceCtrl[0].on == true && sourceCtrl[1].on == true)
            {
                on = true;
                timeout = 0f;
            }
        }
    }
}
