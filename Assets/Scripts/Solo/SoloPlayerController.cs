using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;



public class SoloPlayerController : MonoBehaviour {

	public CharacterController characterCtrlr;
    public static SoloPlayerController main;
    public string state = "normal";

    [SerializeField] GameObject recentEchoLoop;
    [SerializeField] int activeTimeLoops = 0;
    public int maxTimeLoops = 1;
	public float moveSpd;
    public float grabDist;
    public bool holding = false;
    public float holdDist = 2.5f;
    Transform pickupObj;
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

    public GameObject echoPrefab;

    public Transform camObj;
    public float h, v;

    [SerializeField] Animator animCtrl;

    int rewindSteps = 0;
    public float rewindCounter = 0;
    public int rewindLimit = 7;
    int maxAmountOfSteps = 300;
    int sendStep = 0;
    object[,] recordArray = new object[1000,6];
    object[,] sendArray = new object[1000, 6];
    object[] tempArray = new object[6];
    public List<GameObject> echoList = new List<GameObject>();
    public CheckpointControl checkpoint;

    [SerializeField] GameObject GameControllerPrefab;
    SoloAiming horzAim, vertAim;
    

    private void Awake()
    {
        main = this;
        if (GameObject.Find("GameController") == null)
        {
            Instantiate(GameControllerPrefab);
        }
    }
    // Use this for initialization
    void Start () {


		characterCtrlr = GetComponent<CharacterController> ();
        camObj = transform.GetChild(0);
        camOffset = cam.transform.position - transform.position;

        

        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoloGameController>();

        // Get collider component
        coll = GetComponent<CapsuleCollider>();
        // Get Aiming scripts
        horzAim = GetComponent<SoloAiming>();
        vertAim = GetComponentInChildren<SoloAiming>();

        ResetArrays();
        
    }
	// Update is called once per frame
	void Update () {

        switch (gameCtrl.gameState)
        {
            case "start":
                
                break;
            case "live":

                if (active == true)
                {
                    // Get Input
                    h = Input.GetAxis("Horizontal");
                    v = Input.GetAxis("Vertical");

                    //raycast pickup
                    RaycastHit hit;
                    if (holding == false)
                    {
                        if (Input.GetButtonDown("Fire2"))
                        {
                            print("goll");
                            Debug.DrawRay(cam.transform.position, cam.transform.forward * 100f,Color.red);
                            if (Physics.Raycast(cam.transform.position + (cam.transform.forward * 2f), cam.transform.forward, out hit, grabDist))
                            {
                                print("Raycasted!");
                                if (hit.transform.tag == "pickup")
                                {
                                    pickupObj = hit.transform;
                                    pickupObj.GetComponent<Collider>().enabled = false;
                                    holding = true;

                                }
                            }
                        }
                    } else
                    {
                        if (pickupObj != null)
                        {
                            if (Input.GetButtonDown("Fire2"))
                            {
                                holding = false;
                                pickupObj.GetComponent<Collider>().enabled = true;
                                pickupObj = null;
                                
                            }
                            pickupObj.position = (cam.transform.position + cam.transform.forward) * holdDist;

                        }
                    }

                    //reset game button
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        Die();
                        
                    }

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

                    //record aiming script rotation
                    recordArray[0, 4] = horzAim.RotX;
                    recordArray[0, 5] = vertAim.RotY;

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
                    if (moveDirection.x != 0f || moveDirection.z != 0f)
                    {
                        //animCtrl.SetInteger("animState", 1);
                    } else
                    {
                        //animCtrl.SetInteger("animState", 0);
                    }

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
                            recordArray[i, 4] = recordArray[i - 1, 4];
                            recordArray[i, 5] = recordArray[i - 1, 5];
                        }
                    }
                }
                else
                {
                    PlaybackCharacterActions();
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    active = false;
                }
                else
                {
                    //active = true;
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
        state = "rewinding";
        // increment rewindcounter
        rewindCounter += Time.deltaTime;

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
        // Set aiming direction
        horzAim.RotX = (float)recordArray[0, 4];
        vertAim.RotY = (float)recordArray[0, 5];

        
        tempArray[0] = (Vector3)recordArray[0, 0];
        tempArray[1] = (Quaternion)recordArray[0, 1];
        tempArray[2] = (Quaternion)recordArray[0, 2];
        tempArray[3] = (bool)recordArray[0, 3];
        tempArray[4] = (float)recordArray[0, 4];
        tempArray[5] = (float)recordArray[0, 5];

        for (int i = 0; i < maxAmountOfSteps; i++)
        {           
            if ( i != maxAmountOfSteps-1)
            {
                recordArray[i, 0] = recordArray[i + 1, 0];
                recordArray[i, 1] = recordArray[i + 1, 1];
                recordArray[i, 2] = recordArray[i + 1, 2];
                recordArray[i, 3] = recordArray[i + 1, 3];
                recordArray[i, 4] = recordArray[i + 1, 4];
                recordArray[i, 5] = recordArray[i + 1, 5];
            }
        }

        // Loop the information back to the end
        recordArray[maxAmountOfSteps - 1, 0] = (Vector3)tempArray[0];
        recordArray[maxAmountOfSteps - 1, 1] = (Quaternion)tempArray[1];
        recordArray[maxAmountOfSteps - 1, 2] = (Quaternion)tempArray[2];
        recordArray[maxAmountOfSteps - 1, 3] = (bool)tempArray[3];
        recordArray[maxAmountOfSteps - 1, 4] = (float)tempArray[4];
        recordArray[maxAmountOfSteps - 1, 5] = (float)tempArray[5];  


        sendArray[sendStep, 0] = tempArray[0];
        sendArray[sendStep, 1] = tempArray[1];
        sendArray[sendStep, 2] = tempArray[2];
        sendArray[sendStep, 3] = tempArray[3];
        sendArray[sendStep, 4] = tempArray[4];
        sendArray[sendStep, 5] = tempArray[5];

        sendStep++;

        if (Input.GetButtonUp("Fire1") || rewindCounter >= rewindLimit)
        {
            state = "normal";
            CreateTimeLoop();
            sendStep = 0;
            rewindCounter = 0f;
        }

    }
    public void Die()
    {
        transform.position = checkpoint.gameObject.transform.position;
        transform.rotation = checkpoint.gameObject.transform.rotation;
        echoList.Clear();
        activeTimeLoops = 0;
        ResetArrays();
        SoloGameController.main.gameState = "reset";
    }
    void CreateTimeLoop()
    {
        active = true;
        if (activeTimeLoops < maxTimeLoops)
        {
            activeTimeLoops++;
            GameObject inst = Instantiate(echoPrefab, transform.position, transform.rotation);
            recentEchoLoop = inst;
            EchoController echoCtrl = inst.GetComponent<EchoController>();
            
            echoList.Insert(0, inst);

            for (int i = 0; i < sendStep; i++)
            {
                echoCtrl.recordArray[i, 0] = (Vector3)sendArray[i, 0];
                echoCtrl.recordArray[i, 1] = (Quaternion)sendArray[i, 1];
                echoCtrl.recordArray[i, 2] = (Quaternion)sendArray[i, 2];
                echoCtrl.recordArray[i, 3] = (bool)sendArray[i, 3];
                echoCtrl.recordArray[i, 4] = (float)sendArray[i, 4];
                echoCtrl.recordArray[i, 5] = (float)sendArray[i, 5];

                echoCtrl.endStep = sendStep;
            }
        } else if (activeTimeLoops >= maxTimeLoops)
        {
            
            //destroy oldest echo gameobject
            Destroy(echoList[maxTimeLoops-1]);
            //remove oldest echo
            echoList.RemoveAt(maxTimeLoops-1);

            // create new echo
            GameObject inst = Instantiate(echoPrefab, transform.position, transform.rotation);
            recentEchoLoop = inst;
            EchoController echoCtrl = inst.GetComponent<EchoController>();
            //add new echo to list.
            echoList.Insert(0, inst);
            

            for (int i = 0; i < sendStep; i++)
            {
                echoCtrl.recordArray[i, 0] = (Vector3)sendArray[i, 0];
                echoCtrl.recordArray[i, 1] = (Quaternion)sendArray[i, 1];
                echoCtrl.recordArray[i, 2] = (Quaternion)sendArray[i, 2];
                echoCtrl.recordArray[i, 3] = (bool)sendArray[i, 3];
                echoCtrl.recordArray[i, 4] = (float)sendArray[i, 4];
                echoCtrl.recordArray[i, 5] = (float)sendArray[i, 5];

                echoCtrl.endStep = sendStep;
            }
        }

        

        

    }
	void SetCameraOffset()
	{	cam.transform.position = transform.position + camOffset;
	}
    void ResetArrays()
    {
        ///// CHEWCK ME
        //initialise array values.
        for (int i = 0; i < maxAmountOfSteps; i++)
        {
            recordArray[i, 0] = Vector3.zero;
            recordArray[i, 1] = Quaternion.identity;
            recordArray[i, 2] = Quaternion.identity;
            recordArray[i, 3] = false;
            recordArray[i, 4] = 0f;
            recordArray[i, 5] = 0f;

        }

        tempArray[0] = Vector3.zero;
        tempArray[1] = Quaternion.identity;
        tempArray[2] = Quaternion.identity;
        tempArray[3] = false;
        tempArray[4] = 0f;
        tempArray[5] = 0f;
    }

    public void DestroyComponentsAtLayerEnd()
    {   // are we even using this for solo mode?
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "key")
        {
            Destroy(other.gameObject);
            SoloGameController.main.keys += 1;
        }
    }

}
