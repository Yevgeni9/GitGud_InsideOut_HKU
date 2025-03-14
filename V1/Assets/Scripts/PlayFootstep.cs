using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dit gebruik ik niet meer
public class PlayFootstep : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;

    public void playFootstep()
    {
        audioSource.Play();
    }

    public void PlayFootstep2()
    {
        audioSource2.Play();
    }
}
