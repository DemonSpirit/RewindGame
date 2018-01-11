using UnityEngine;
using System.Collections;

public class LaserCatchControl : ActivatableObject
{
    

    private void Update()
    {
        
        if (on)
        {
            timeout -= Time.deltaTime;
            if(timeout <= 0) on = false;
        }
    }

}
