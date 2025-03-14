using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Voor het beheren van de regen sfx aan het begin en einde van scene 3
public class RainRamp : MonoBehaviour
{
    public AudioSource rainAudioSource;
    public float targetVolume = 0.35f;
    public float fadeDuration = 1f;

    private void Start()
    {
        if (rainAudioSource != null)
        {
            rainAudioSource.volume = 0f;
            StartCoroutine(FadeInRainSound());
        }
    }

    private IEnumerator FadeInRainSound()
    {
        float elapsedTime = 0f;
        float initialVolume = rainAudioSource.volume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            rainAudioSource.volume = Mathf.Lerp(initialVolume, targetVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        rainAudioSource.volume = targetVolume;
    }

    public void OnRainFadeOut()
    {
        targetVolume = 0f;
        fadeDuration = 0.2f;
        StartCoroutine(FadeOutRainSound());
    }

    private IEnumerator FadeOutRainSound()
    {
        float elapsedTime = 0f;
        float initialVolume = rainAudioSource.volume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            rainAudioSource.volume = Mathf.Lerp(initialVolume, targetVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        rainAudioSource.volume = targetVolume;
    }
}
