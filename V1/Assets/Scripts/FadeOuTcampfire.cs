using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Voor het vuur sfx die zachter moet worden in het einde van de scene
public class FadeOuTcampfire : MonoBehaviour
{
    public AudioSource Campfiresfx;
    public float targetVolume = 0.35f;
    public float fadeDuration = 7f;

    public void OnRainFadeOut()
    {
        targetVolume = 0f;
        fadeDuration = 2f;
        StartCoroutine(FadeOutRainSound(5f));
    }

    private IEnumerator FadeOutRainSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsedTime = 0f;
        float initialVolume = Campfiresfx.volume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Campfiresfx.volume = Mathf.Lerp(initialVolume, targetVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        Campfiresfx.volume = targetVolume;
    }
}
