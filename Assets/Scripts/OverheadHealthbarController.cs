using UnityEngine;
using System.Collections;

public class OverheadHealthbarController : MonoBehaviour
{
    SpriteRenderer rend;
    
    public Sprite[] healthbarSprites;
    GameControl gameCtrl;
    PlayerController playerCtrl;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        playerCtrl = GetComponentInParent<PlayerController>();
        gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();
    }
    void Update()
    {

        switch (gameCtrl.gameState)
        {
            case "pre-live":
                rend.sprite = healthbarSprites[healthbarSprites.Length - 1];
                break;
            case "live":

                //rotate towards activeinst
                //transform.rotation = Quaternion.FromToRotation(transform.position, gameCtrl.activeInst.transform.position);
                float hpPerc = ((float)playerCtrl.health / playerCtrl.maxHealth) * 100f;

				float indexDivider = 100 / (healthbarSprites.Length - 1);
				int spriteIndex = (int) ((hpPerc + 2.5f) / indexDivider);
				//print("spriteIndex " + spriteIndex);
				rend.sprite = healthbarSprites[spriteIndex];

                transform.LookAt(gameCtrl.activeInst.transform);
                
                break;
        }
        
        
    }
}
