using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public CharacterController characterCtrlr;
	public float moveSpd;
	public Vector3 moveDirection = Vector3.zero;
	public float jumpSpd = 16f;
	public float gravity = 20f;
	public Vector3 camOffset = Vector3.zero;
	public Camera cam;
    public bool active = false;
    public GameControl gameCtrl;
    Transform camObj;
    float h, v;

    int maxSteps;
    object[,] recordArray = new object[1800,4];

	// Use this for initialization
	void Start () {
		characterCtrlr = GetComponent<CharacterController> ();
        camObj = transform.GetChild(0);
        camOffset = cam.transform.position - transform.position;

        GameObject gamecontrollerObj = GameObject.Find("GameController");
        gameCtrl = gamecontrollerObj.GetComponent<GameControl>();

        //initialise array values.
        for (int i = 0; i < gameCtrl.maxSteps; i++)
        {
            for (int ii = 0; ii < 4; ii++)
            {
                //inputArray[i, ii] = 0;
                recordArray[i, ii] = null;
                Debug.Log(recordArray[i, ii]);
            }

        }
    }
	

	// Update is called once per frame
	void FixedUpdate () {

        if (active == true && gameCtrl.gameState == "live")
        {
            // Get Input
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // Record pos and rot
            recordArray[gameCtrl.step, 0] = transform.position;
            recordArray[gameCtrl.step, 1] = transform.rotation;
            // record camera rot
            recordArray[gameCtrl.step, 2] = camObj.rotation;
            //Set Cam Dist
            SetCameraOffset();

            // Check Gravity
            if (characterCtrlr.isGrounded == true)
            {
                moveDirection = new Vector3(h, 0, v);
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpd;
                }
            }
            else
            {
                moveDirection.x = h;
                moveDirection.z = v;
                moveDirection.y -= gravity * Time.deltaTime;
            }

            //  Apply movement
            moveDirection.x *= moveSpd;
            moveDirection.z *= moveSpd;
            moveDirection = transform.TransformDirection(moveDirection);
            characterCtrlr.Move(moveDirection * Time.deltaTime);

        } else if (active == false && gameCtrl.gameState == "live")
        {
            transform.position = (Vector3)recordArray[gameCtrl.step, 0];
            transform.rotation = (Quaternion)recordArray[gameCtrl.step, 1];
            camObj.rotation = (Quaternion)recordArray[gameCtrl.step, 2];

        }
	}

	void SetCameraOffset()
	{	cam.transform.position = transform.position + camOffset;
	}

    public void DestroyComponentsAtLayerEnd()
    {
        Debug.Log("Destroy components Called");

        camObj.GetComponent<Camera>().enabled = false;
        Destroy(GetComponent<Aiming>());
        Destroy(camObj.GetComponent<AudioListener>());
        Destroy(camObj.GetComponent<Aiming>());
    }
}
