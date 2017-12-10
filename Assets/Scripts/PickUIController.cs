using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickUIController : MonoBehaviour
{

    GameControl gameCtrl;
    Text pickText;
    Image pickIcon;
    public Sprite[] characterIcons;
    public string[] characterNames;
    // Use this for initialization
    void Start()
    {
        gameCtrl = GameControl.main;
        pickText = GetComponentInChildren<Text>();
        pickIcon = GetComponentInChildren<Image>();
    }

    public void ChangePickUI(int pick)
    {
        pickText.text = characterNames[gameCtrl.pick];
        pickIcon.sprite = characterIcons[gameCtrl.pick];

    }
}
