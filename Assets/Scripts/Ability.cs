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
        GameObject playerHitPFX;
        GameObject woodHitPFX;

        GameControl gameCtrl;

        void Start()
        {
            gameCtrl = GameControl.main;
            playerHitPFX = gameCtrl.playerHitPFX;
            woodHitPFX = gameCtrl.woodHitPFX;
        }

        public void Use(PlayerController playerCtrl, int ID)
        {

            switch (ID)
            {
                case 0:
                    Debug.Log("No AbilityID");
                    break;
                case 1:
                    //Debug.Log("Projectile Shoot Ability: " + playerCtrl.name);

                    if (playerCtrl.ammo > 0)
                    {
                        playerCtrl.ammo -= 1;
                        // get the camera angle
                        Quaternion camRot = playerCtrl.camObj.transform.rotation;
                        Quaternion playerRot = playerCtrl.transform.rotation;
                        // create rocket at that angle
                        inst = Instantiate(playerCtrl.bullet, playerCtrl.camObj.position + (playerCtrl.camObj.forward * 1.5f), new Quaternion(camRot.x, playerRot.y, playerRot.z, playerRot.w));
                        // set rocket team
                        inst.GetComponent<Projectile>().team = playerCtrl.team;
                        // set if rocket is a rewind or not
                        if (GameControl.main.gameState == "live") inst.GetComponent<Projectile>().real = true;
                        // call sfx
                        
                        FMODUnity.RuntimeManager.PlayOneShot(AudioControl.instance.rocketFireSFX,transform.position);
                    } else
                    {
                        //print("no ammo!");
                        FMODUnity.RuntimeManager.PlayOneShot(AudioControl.instance.rocketFireNoAmmoSFX, transform.position);
                    }

                    
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

                        if (hit.transform.tag == "Environment_Wood")
                        {

                            Destroy(Instantiate(woodHitPFX, hit.point, hit.transform.rotation), 0.4f);
                        }
                        // If the hit obj is a player
                        if (hit.transform.tag == "Player")
                        {

                            // Get PlayerController and damage it.
                            PlayerController hitCtrl = hit.transform.GetComponent<PlayerController>();

                            if (hitCtrl.alive == true && hitCtrl.team != playerCtrl.team)
                            {

                                // apply damage to hit target.

                                hitCtrl.health -= playerCtrl.abilityDMG[0];
                                //print("hitCtrl" + hitCtrl.health);

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
                                    Instantiate(playerHitPFX, hit.point, hit.transform.rotation);
                                }
                            }
                        }
                        if (hit.transform.tag == "Scorezone" && gameCtrl.gameState == "live")
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
