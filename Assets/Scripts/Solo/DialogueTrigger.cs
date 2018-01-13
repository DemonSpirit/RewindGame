using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] string[] str;
    [SerializeField] public bool triggered = false;
    float alpha = 1f;
    [SerializeField] float holdTime = 1.5f;
    [SerializeField]public bool resetable = false;

    private void Start()
    {
        text = SoloGameController.main.textBox;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && triggered == false)
        {
            //Text text = SoloGameController.main.textBox;
            //text.text = str[0];

            DialogueTextAnimator animCtrl = text.gameObject.GetComponent<DialogueTextAnimator>();
            for (int i = 0; i < str.Length; i++)
            {
                animCtrl.strList.Add(str[i]);
            }
            animCtrl.animStage = 0;
            animCtrl.holdTime = holdTime;
            triggered = true;
            animCtrl.curTrigger = this;

        }
    }

}
