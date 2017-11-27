using UnityEngine;
using System.Collections;

public class EchoController : MonoBehaviour
{
    public object[,] recordArray = new object[1000, 6];
    public int step = 0;
    public int endStep = 0;
   
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (step < 0) step = endStep-1;
        // Get events for recordArray and set them.
        transform.position = (Vector3)recordArray[step, 0];
        transform.rotation = (Quaternion)recordArray[step, 1];
        //camObj.rotation = (Quaternion)recordArray[0, 2];
        // Check recordArray if weapon was pressed.
        /*
        if ((bool)recordArray[0, 3] == true)
        {
            UseWeapon();
        }
        */
        // Set aiming direction
        //horzAim.RotX = (float)recordArray[0, 4];
        //vertAim.RotY = (float)recordArray[0, 5];

        step--;
        
    }
}
