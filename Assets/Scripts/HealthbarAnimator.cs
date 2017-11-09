using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthbarAnimator : MonoBehaviour
{
    public Sprite[] images;
    public Image spriteCtrl;
    public int i = 0;

    // Use this for initialization
    void Start()
    {
        spriteCtrl = GetComponent<Image>();
        Debug.Log(images.Length);
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i >= 7) i = 0;
        spriteCtrl.sprite = images[i];
        
    }
}
