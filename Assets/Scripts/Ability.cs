using UnityEngine;
using System.Collections;

namespace Abilities
{
    public class Ability : MonoBehaviour
    {
        GameObject inst;
        public GameObject healBullet;

        public void Use(PlayerController playerCtrl, int ID)
        {
            
            switch (ID)
            {
                case 0:
                    Debug.Log("No AbilityID");
                    break;
                case 1:
                    Debug.Log("Shoot Ability: " + playerCtrl.name);
                    inst = Instantiate(playerCtrl.bullet, playerCtrl.camObj.position + (playerCtrl.camObj.forward * 1.5f), playerCtrl.camObj.rotation);
                    inst.GetComponent<BulletControl>().team = playerCtrl.team;
                    break;
                case 2:
                    Debug.Log("Block Ability");
                    break;
                case 3:
                    Debug.Log("Heal Ability");
                    inst = Instantiate(healBullet, playerCtrl.camObj.position + (playerCtrl.camObj.forward * 1.5f), playerCtrl.camObj.rotation);
                    inst.GetComponent<BulletControl>().team = playerCtrl.team;
                    break;

            }
            
        }

    }
}
