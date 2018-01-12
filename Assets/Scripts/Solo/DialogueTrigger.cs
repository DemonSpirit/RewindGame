using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] string str = "";
    bool triggered = false;

    
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && triggered == false)
        {
            SoloGameController.main.textBox.text = str;
            //SoloGameController.main.textBox.gameObject.GetComponent<Dialog>

            
        }
    }

}
