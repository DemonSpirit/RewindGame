using UnityEngine;
using System.Collections;

public class OverheadHealthbarController : MonoBehaviour
{
    SpriteRenderer rend;
    
    public Sprite[] healthbarSprites;
    GameControl gameCtrl;
    PlayerController playerCtrl;
    float eachChunkPerc;
    // Use this for initialization

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        playerCtrl = GetComponentInParent<PlayerController>();
        gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();

        Debug.Log(healthbarSprites.Length);
        eachChunkPerc = 100 / healthbarSprites.Length;
        Debug.Log(eachChunkPerc);
    }

    // Update is called once per frame
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
				print("spriteIndex " + spriteIndex);
				rend.sprite = healthbarSprites[spriteIndex];
                
                break;
        }
        
        
    }
}
