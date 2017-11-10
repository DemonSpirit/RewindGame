using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthbarAnimator : MonoBehaviour
{
    public Sprite[] images;
    public Image spriteCtrl;
    public int i = 0;

    public int segmentAmount = 50;
    public GameObject owner = null;

    // Use this for initialization
    void Start()
    {
        spriteCtrl = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {

       

        i++;
        if (i >= 7) i = 0;
        spriteCtrl.sprite = images[i];
        
    }
}
