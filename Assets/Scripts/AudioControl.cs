using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour
{
    public static AudioControl instance;
    [FMODUnity.EventRef] public string pickupSFX;

    void Awake()
    {
        instance = this;
    }

    public static void Sound(string soundEvent)
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundEvent);
    }
}