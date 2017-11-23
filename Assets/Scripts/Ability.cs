using UnityEngine;
using System.Collections;

namespace Abilities
{
    public class Ability : MonoBehaviour
    {
        [SerializeField]
        int amtOfSkills = 5;
        [FMODUnity.EventRef]
        public string[] sfx;
        [FMODUnity.EventRef]
        public string hitMarkerSFX;

        GameObject inst;
        public GameObject healBullet;
        public GameObject playerHitParticleFX;
        GameControl gameCtrl;

        void Start()
        {
            gameCtrl = GameObject.Find("GameController").GetComponent<GameControl>();
        }

        public void Use(PlayerController playerCtrl, int ID)
        {
            
            switch (ID)
            {
                case 0:
                    Debug.Log("No AbilityID");
                    break;
                case 1:
                    Debug.Log("Projectile Shoot Ability: " + playerCtrl.name);
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
                case 4:
                    //Debug.Log("Raycast Shot Ability");

                    // raycast
                    RaycastHit hit;
                    // play sound
                    FMODUnity.RuntimeManager.PlayOneShot(sfx[ID], transform.position);

                    if (Physics.Raycast(playerCtrl.cam.transform.position, playerCtrl.cam.transform.forward, out hit, 50f))
                    {
                        // Debug Ray
                        //Debug.DrawRay(playerCtrl.cam.transform.position, playerCtrl.cam.transform.forward*50f,Color.green,1f);
                        

                        // If the hit obj is a player
                        if (hit.transform.tag == "Player")
                        {   
							
                            // Get PlayerController and damage it.
                            PlayerController hitCtrl = hit.transform.GetComponent<PlayerController>();

                            if (hitCtrl.alive == true && hitCtrl.team != playerCtrl.team) {

                                // apply damage to hit target.
								
                                hitCtrl.health -= playerCtrl.abilityDMG[0];
								print("hitCtrl" + hitCtrl.health);

                                if (hitCtrl.health <= 0)
                                {
                                    if (playerCtrl.team == 1) gameCtrl.team1money += hitCtrl.bounty;
                                    if (playerCtrl.team == 2) gameCtrl.team2money += hitCtrl.bounty;

                                    // kill confirm set elimation text and gain money
                                    if (playerCtrl.active == true)
                                    {
										// Sets raycast layer to ignoreRast
										hit.transform.gameObject.layer = 2;

                                        GameObject.Find("EliminationText").GetComponent<EliminationTextAnimator>().SetText(hitCtrl.bounty.ToString() + "$ " + hitCtrl.agentName.ToString());
                                    }
                                }

                                //play hitmarker sound
                                if (playerCtrl.active == true) FMODUnity.RuntimeManager.PlayOneShot(hitMarkerSFX, transform.position);

                                // spawn hit effect
                                if (hitCtrl.active == false)
                                {
                                    Instantiate(playerHitParticleFX,hit.point,hit.transform.rotation);
                                }
                            }
                        }
                        if (hit.transform.tag == "Scorezone")
                        {
                            GoalZoneController goalCtrl = hit.transform.GetComponent<GoalZoneController>();
                            if (goalCtrl.team != playerCtrl.team)
                            {
                                if (playerCtrl.team == 1) gameCtrl.team1points += 10;
                                if (playerCtrl.team == 2) gameCtrl.team2points += 10;
                            }
                        }
                        
                    }
                    break;

            }
            
        }

    }
}
