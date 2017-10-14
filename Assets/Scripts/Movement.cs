using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public CharacterController characterCtrlr;
	public float moveSpd;
	public Vector3 moveDirection = Vector3.zero;
	public float jumpSpd = 16f;
	public float gravity = 20f;
	public Vector3 camOffset = Vector3.zero;
	public Camera cam;
    public bool active = false;
    public GameControl gameCtrl;
    float h, v;

    int maxSteps;
    float[,] inputArray = new float[1800, 4];

    // TO DO U NEED TO MAKE THE ARRAY AN ARRAY OF OBJECTS, SINCE EVERYTHING IS AN OBJECT WAOW
    // WHEN U ARE CALLING THE INFORMATION MAKE USRE U CAST THE DATA SO THAT THE THING TACN TDO ITS TING.

	// Use this for initialization
	void Start () {
		characterCtrlr = GetComponent<CharacterController> ();
        camOffset = cam.transform.position - transform.position;

        GameObject gamecontrollerObj = GameObject.Find("GameController");
        gameCtrl = gamecontrollerObj.GetComponent<GameControl>();

        //initialise array values.
        for (int i = 0; i < gameCtrl.maxSteps; i++)
        {
            for (int ii = 0; ii < 4; ii++)
            {
                inputArray[i, ii] = 0;
            }

        }
    }
	
	// Update is called once per frame
	void Update () {

        if (active == true && gameCtrl.gameState == "live")
        {
            
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            inputArray[gameCtrl.step, 0] = h;
            inputArray[gameCtrl.step, 1] = v;
            //inputArray[gameCtrl.step, 2] = transform.rotation;
            
        } else if (active == false && gameCtrl.gameState == "live")
        {
            h = inputArray[gameCtrl.step, 0];
            v = inputArray[gameCtrl.step, 1];

        }
		

		//call fmod event : footstep.event

		if (characterCtrlr.isGrounded == true) 
		{	
			moveDirection = new Vector3 (h, 0, v);
			if (Input.GetButton ("Jump")) 
			{
				moveDirection.y = jumpSpd;
			}
		} else
		{	moveDirection.x = h;
			moveDirection.z = v;
			moveDirection.y -= gravity*Time.deltaTime;
		}
			
		moveDirection.x *= moveSpd;
		moveDirection.z *= moveSpd;
		moveDirection = transform.TransformDirection (moveDirection);
		characterCtrlr.Move (moveDirection *  Time.deltaTime);

		SetCameraOffset ();




	}

	void SetCameraOffset()
	{	cam.transform.position = transform.position + camOffset;
	}

    void DestroyCamera()
    {
        Transform cam = transform.GetChild(0);
        Destroy(cam);
    }
}
