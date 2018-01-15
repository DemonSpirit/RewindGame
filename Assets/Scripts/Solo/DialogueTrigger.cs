using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] string[] str;
    [SerializeField] public bool triggered = false;
    [SerializeField] int runAtKeyStage = 0;
    float alpha = 1f;
    [SerializeField] float holdTime = 1.5f;
    [SerializeField]public bool resetable = false;
    [SerializeField] float countdown = 0;
    [SerializeField] float countdownTime;
    [SerializeField] float toNarrativeStage;

    private void Start()
    {
        text = GameObject.Find("DialogueText").GetComponent<Text>();

    }

    private void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        } else if (countdown < 0)
        {
            SoloGameController.main.narrativeStage = toNarrativeStage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && triggered == false && SoloGameController.main.keys == runAtKeyStage)
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
            countdownTime = holdTime * str.Length;
            countdown = countdownTime;

        }
    }

}
