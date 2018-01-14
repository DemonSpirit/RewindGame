using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour
{
    public static AudioControl instance;
    [FMODUnity.EventRef] public string pickupSFX;
    [FMODUnity.EventRef] public string rocketFireSFX;
    [FMODUnity.EventRef] public string rocketFireNoAmmoSFX;
    [FMODUnity.EventRef] public string ambientRain;
    [FMODUnity.EventRef] public string painHitSFX;
    [FMODUnity.EventRef] public string footstepSFX;
    [FMODUnity.EventRef] public string blockSFX;
    [FMODUnity.EventRef] public string rewindLoop;


    void Awake()
    {
        instance = this;
    }

    public static void Sound(string soundEvent)
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundEvent);
    }
}