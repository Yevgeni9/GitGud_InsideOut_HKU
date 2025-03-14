using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script zorgt ervoor dat de voetstappen een willekeurige clip een pitch krijgen
// Dit werkt echt prachtig
public class Footstep : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> footstepClips;
    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 1.5f;

    public void PlayFootstep()
    {
        if (footstepClips.Count > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, footstepClips.Count);
            float randomPitch = Random.Range(minPitch, maxPitch);
            audioSource.pitch = randomPitch;
            audioSource.PlayOneShot(footstepClips[randomIndex], Random.Range(1.5f, 2f));
        }
    }

    public void PlayRunningFootstep()
    {
        if (footstepClips.Count > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, footstepClips.Count);
            float randomPitch = Random.Range(minPitch, maxPitch);
            audioSource.pitch = randomPitch;
            audioSource.PlayOneShot(footstepClips[randomIndex], Random.Range(1.5f, 2f));
        }
    }
}