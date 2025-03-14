using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dit script wordt voor zo ver ik weet niet gebruik
// Testen of dialoog met fade beter is dan omhoog/omlaag bewegen
public class Fades : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public Transform player;


    public float fadeDuration = 1.0f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartFade()
    {
        StartCoroutine(FadeOut());
    }

    public void EndFade()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        gameObject.SetActive(true);
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    private void Update()
    {
        if (player.position.z > 5)
        {
            StartFade();
        }

        if (player.position.z > 35)
        {
            EndFade();
        }
    }
}
