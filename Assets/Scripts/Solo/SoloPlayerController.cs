using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;



public class SoloPlayerController : MonoBehaviour {

	public CharacterController characterCtrlr;
    

	public float moveSpd;
	public Vector3 moveDirection = Vector3.zero;
	public float jumpSpd = 16f;
	public float gravity = 20f;
	public Vector3 camOffset = Vector3.zero;
	public Camera cam;
    public bool active = true;
    bool recording = false;
    public SoloGameController gameCtrl;

    CapsuleCollider coll;
    public Renderer rend;
    

    public Transform camObj;
    public float h, v;


    int maxAmountOfSteps = 300;
    object[,] recordArray = new object[1000,4];
    object[] tempArray = new object[4];

    // Use this for initialization
    void Start () {


		characterCtrlr = GetComponent<CharacterController> ();
        camObj = transform.GetChild(0);
        camOffset = cam.transform.position - transform.position;

        gameCtrl = GameObject.Find("GameController").GetComponent<SoloGameController>();

        // Get collider component
        coll = GetComponent<CapsuleCollider>();

        ///// CHEWCK ME
        //initialise array values.
        for (int i = 0; i < maxAmountOfSteps; i++)
        {
            recordArray[i, 0] = Vector3.zero;
            recordArray[i, 1] = Quaternion.identity;
            recordArray[i, 2] = Quaternion.identity;
            recordArray[i, 3] = false;

        }
       
        tempArray[0] = Vector3.zero;
        tempArray[1] = Quaternion.identity;
        tempArray[2] = Quaternion.identity;
        tempArray[3] = false;
    }
	// Update is called once per frame
	void Update () {

        switch (gameCtrl.gameState)
        {
            case "start":
                
                break;
            case "live":

                if (Input.GetButton("Fire2"))
                {
                    active = false;
                } else
                {
                    active = true;
                }

                if (active == true)
                {
                    // Get Input
                    h = Input.GetAxis("Horizontal");
                    v = Input.GetAxis("Vertical");

                    

                    // Record pos and rotation
                    recordArray[0, 0] = transform.position;
                    recordArray[0, 1] = transform.rotation;

                    // record camera rotation
                    recordArray[0, 2] = camObj.rotation;

                    // check for and record fire button
                    if (Input.GetButton("Fire1"))
                    {
                        recordArray[0, 3] = true;
                    }

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
                    characterCtrlr.Move(moveDirection * Time.deltaTime * gameCtrl.playbackSpeed);

                    //shuffle recorded steps
                    for (int i = maxAmountOfSteps - 1; i >= 0; i--)
                    {
                        if (i != 0)
                        {                       
                            recordArray[i, 0] = recordArray[i - 1, 0];
                            recordArray[i, 1] = recordArray[i - 1, 1];
                            recordArray[i, 2] = recordArray[i - 1, 2];
                            recordArray[i, 3] = recordArray[i - 1, 3];
                        }
                    }
                }
                else
                {
                    PlaybackCharacterActions();
                }
                break;
            case "pre-rewind":
                break;
            case "rewind":
                
                PlaybackCharacterActions();
                break;
        }
            
	}

    void PlaybackCharacterActions()
    {
        // Get events for recordArray and set them.
        transform.position = (Vector3)recordArray[0, 0];
        transform.rotation = (Quaternion)recordArray[0, 1];
        camObj.rotation = (Quaternion)recordArray[0, 2];
        // Check recordArray if weapon was pressed.
        /*
        if ((bool)recordArray[0, 3] == true)
        {
            UseWeapon();
        }
        */

        
        tempArray[0] = (Vector3)recordArray[0, 0];
        tempArray[1] = (Quaternion)recordArray[0, 1];
        tempArray[2] = (Quaternion)recordArray[0, 2];
        tempArray[3] = (bool)recordArray[0, 3];

        for (int i = 0; i < maxAmountOfSteps; i++)
        {           
            if ( i != maxAmountOfSteps-1)
            {
                recordArray[i, 0] = recordArray[i + 1, 0];
                recordArray[i, 1] = recordArray[i + 1, 1];
                recordArray[i, 2] = recordArray[i + 1, 2];
                recordArray[i, 3] = recordArray[i + 1, 3];
            }
        }

        // Loop the information back to the end
        recordArray[maxAmountOfSteps - 1, 0] = (Vector3)tempArray[0];
        recordArray[maxAmountOfSteps - 1, 1] = (Quaternion)tempArray[1];
        recordArray[maxAmountOfSteps - 1, 2] = (Quaternion)tempArray[2];
        recordArray[maxAmountOfSteps - 1, 3] = (bool)tempArray[3];

    }
	void SetCameraOffset()
	{	cam.transform.position = transform.position + camOffset;
	}

    public void DestroyComponentsAtLayerEnd()
    {
        camObj.GetComponent<Camera>().enabled = false;
        Destroy(GetComponent<Aiming>());
        Destroy(camObj.GetComponent<AudioListener>());
        Destroy(camObj.GetComponent<Aiming>());
        //Destroy FMOD Listener
        //GetComponent<FMOD_Listener>().enabled = false;

    }

    void UseWeapon()
    {
        print("Use Weapon Called");
    }

    
}
