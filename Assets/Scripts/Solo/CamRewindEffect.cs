using UnityEngine;
using UnityEngine.PostProcessing;
using System.Collections;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class CamRewindEffect : MonoBehaviour
{
    PostProcessingProfile prof;
    [SerializeField] float maxChromIntensity = 1f;

    
    void OnEnable()
    {
        var behaviour = GetComponent<PostProcessingBehaviour>();

        if (behaviour.profile == null)
        {
            enabled = false;
            return;
        }

        prof = Instantiate(behaviour.profile);
        behaviour.profile = prof;
    }

    // Update is called once per frame
    void Update()
    {
        if (SoloPlayerController.main.state == "rewinding")
        {
            print("woaahhhhmaan");
            var chrom = prof.chromaticAberration.settings;
            if (chrom.intensity <= maxChromIntensity)
            {
                chrom.intensity += 0.1f;
            }
            
            prof.chromaticAberration.settings = chrom;
        }  else
        {
            var chrom = prof.chromaticAberration.settings;
            if (chrom.intensity > 0)
            {
                chrom.intensity -= 0.2f;
            } else
            {
                chrom.intensity = 0;
            }
            prof.chromaticAberration.settings = chrom;

        }
    }
}
