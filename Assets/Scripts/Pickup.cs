using UnityEngine;
using System.Collections;

public abstract class Pickup: MonoBehaviour
{
    protected GameControl gameCtrl;
    [SerializeField] [FMODUnity.EventRef] string pickupSFX;
    Vector3 startPos;
    Quaternion startRot;
    Material mat;
    bool isClaimed = false;
    [SerializeField] float alphaFade = 0.1f;

    // declare the required functions for child classes.
    public abstract void PickupAction(Collider other);


    private void Start()
    {
        gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();
        startPos = transform.position;
        mat = GetComponent<Renderer>().material;
        
    }
    private void Update()
    {
        
        switch (gameCtrl.gameState)
        {
            case "start":
                Reset();
                break;
            case "pre-rewind":
                Reset();
                break;
            default:
                break;
                
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isClaimed && gameCtrl.gameState == "live")
        {
            print(other.name + " entered " + name);
            isClaimed = true;
            PickupAction(other);
            // fade alpha when collected.
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alphaFade);
            FMODUnity.RuntimeManager.PlayOneShot(pickupSFX);
        }
    }
    void Reset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        // restore alpha to full
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f);
        isClaimed = false;
    }

}   
