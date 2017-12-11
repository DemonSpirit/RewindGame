using UnityEngine;
using System.Collections;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField]
    PlayerController playerCtrl;
    [SerializeField]
    GameControl gameCtrl;
    [SerializeField]
    Animator animator;

    public GameObject muzzlePrefab;
    // Use this for initialization
    void Start()
    {
        gameCtrl = GameControl.main;
        animator = GetComponent<Animator>();
        playerCtrl = GetComponentInParent<PlayerController>();
        //muzzlePrefab.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {   
        if (playerCtrl.animShooting == true )
        {
            animator.SetBool("shooting", true);
            
        } else
        {
            animator.SetBool("shooting", false);
            //muzzlePrefab.SetActive(false);
        }
    }
}
