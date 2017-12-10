using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EliminationTextAnimator : MonoBehaviour
{
    GameControl gameCtrl;
    Text txt;
    [SerializeField]
    bool animated = false;
    bool fadeOut = false;
    
    Color baseColor;
    public float fadeSpeed = 0.01f;
    public float moveSpeed = 0.01f;
    float hanging = 0f;
    public float hangTime = 1f;
    Vector3 basePos;
    // Use this for initialization
    void Start()
    {
        txt = GetComponent<Text>();
        baseColor = txt.color;
        basePos = transform.position;
        gameCtrl = GameControl.main;
    }

    // Update is called once per frame
    void Update()
    {
       if (gameCtrl.gameState == "pre-pick") {
            txt.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0f);
        }
       if (animated == true && gameCtrl.gameState == "live")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
            if (fadeOut == false) hanging += Time.deltaTime;
            if (hanging >= hangTime) fadeOut = true;

            if (fadeOut == true)
            {
                txt.color = new Color(baseColor.r, baseColor.g, baseColor.b, txt.color.a - fadeSpeed);
                if (txt.color.a <= 0)
                {
                    animated = false;
                    
                }
            }
            
        }
    }
    public void SetText(string str)
    {
        txt.text = str + " Eliminated";
        txt.color = baseColor;
        animated = true;
        // reset animating variables
        hanging = 0f;
        fadeOut = false;
        transform.position = basePos;
    }
}
