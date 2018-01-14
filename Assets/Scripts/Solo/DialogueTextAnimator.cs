using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueTextAnimator : MonoBehaviour
{
    bool animate = true;
    public int animStage = -1;
    [SerializeField] Vector3 startPos;
    [SerializeField] float moveAmount = 100f;
    [SerializeField] Vector3 targetPos;
    public float holdTime = 2f;
    RectTransform rTransform;
    float counter = 0f;
    [SerializeField] float riseTime = 1f;
    [SerializeField] float riseSpd = 0.1f;
    Color startCol,col;
    public List<string> strList = new List<string>();
    Text text;
    float alpha = 0f;
    [SerializeField] float alphaFadeSpd = 0.05f;
    public DialogueTrigger curTrigger;
    // Use this for initialization
    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        startPos = rTransform.anchoredPosition3D;
        targetPos = rTransform.anchoredPosition3D + (Vector3.up * moveAmount);
        text = GetComponent<Text>();
        startCol = text.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            switch (animStage)
            {
                case 0:
                    rTransform.anchoredPosition3D = startPos;
                    text.color = startCol;
                    alpha = 0f;
                    text.text = strList[0];
                    strList.RemoveAt(0);
                    animStage = 1;
                    break;
                case 1:

                    alpha += alphaFadeSpd;
                    rTransform.anchoredPosition3D = Vector3.Lerp(rTransform.anchoredPosition3D, targetPos, riseSpd);
                    text.color = new Color(startCol.r, startCol.g, startCol.b, alpha);

                    counter += Time.deltaTime;
                    if (counter >= riseTime)
                    {
                        animStage = 2;
                        counter = 0;
                    }
                    
                    break;
                case 2:
                    counter += Time.deltaTime;
                    if (counter >= holdTime)
                    {
                        counter = 0;
                        animStage = 3;
                    }
                    break;
                case 3:
                    alpha -= alphaFadeSpd;
                    text.color = new Color(startCol.r, startCol.g, startCol.b, alpha);
                    if (alpha <= 0f)
                    {
                        if (strList.Count > 0)
                        {
                            animStage = 0;
                        } else
                        {
                            animStage = -1;
                        }
                        
                        if (curTrigger.resetable == true) curTrigger.triggered = false;
                    }
                    break;
                
            }
        }
    }
}
