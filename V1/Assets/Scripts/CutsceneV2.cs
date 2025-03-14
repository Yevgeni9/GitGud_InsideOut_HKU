using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

// Script is voor de (mini)cutscene als je gedetecteerd wordt
// Gedaan via code want Timelines in Unity zijn verwarrend
// (Voor de refactor) Dit is de verbeterde versie van mijn cutscene script, functioneel is er niks veranderd

public class CutsceneV2 : MonoBehaviour
{
    // De vier personages die 'uit' gezet moeten worden als de cutscene begint
    [SerializeField] private GameObject char1;
    [SerializeField] private GameObject char2;
    [SerializeField] private GameObject char3;
    [SerializeField] private GameObject char4;

    // Objecten die moeten veranderen tijdens de cutscene
    [SerializeField] private GameObject cutsceneCharacters;
    [SerializeField] private GameObject bloodpool;
    [SerializeField] private GameObject rain;
    [SerializeField] private Light muzzleFlash;

    // UnityEvents voor acties buiten dit script
    public UnityEvent SayDeathLine;
    public UnityEvent RainVolumeDown;

    // Soundeffects die gebruikt moeten worden
    [SerializeField] private AudioSource alarm;
    [SerializeField] private AudioSource shot;

    // Om de Motion capture animaties te kunnen spelen
    [SerializeField] private Animator animator;

    // Booleans die ervoor zorgen dat if-statements maar één keer uitgevoerd worden
    private bool startedCutscene = false;
    private bool cutscenePart1 = false;
    private bool cutscenePart2 = false;
    private bool cutscenePart3 = false;
    private bool cutscenePart4 = false;
    private bool cutscenePart5 = false;

    // Vector3 voor de bloedvlek die moet vergroten na het schot
    [SerializeField] private Vector3 bloodTargetScale = new Vector3(2f, 0.05f, 2f);

    [SerializeField] private float muzzleFlashDuration = 0.1f;
    [SerializeField] private float bloodpoolDuration = 5f;
    private float timerCutscene = 0f;

    void Start()
    {
        cutsceneCharacters.SetActive(false);
        bloodpool.SetActive(false);
        muzzleFlash.enabled = false;
    }
    
    void Update()
    {
        // Dit kan waarschijnlijk efficiënter opgezet worden, maar ik ben onzeker hoe dit gedaan kan worden
        if (startedCutscene)
        {
            timerCutscene += Time.deltaTime;
        }
    }

    public void StartCutscene()
    {
        // Start de fade naar zwart
        animator.SetTrigger("PlayerDetected");
        
        if (!startedCutscene)
        {
            alarm.Play();
            startedCutscene = true;
        }

        StartCoroutine(CutsceneTimer());
    }

    // Coroutine die de muzzle flash van het pistool regelt
    private IEnumerator LightFlashCoroutine()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.enabled = false;
    }

    // Functie voor de cutscene zelf, nadat de timer bepaalde waardes overtreft gebeuren er nieuwe onderdelen van de cutscene
    private IEnumerator CutsceneTimer()
    {
        // Wacht tot het scherm zwart is om de huidige characters uit te zetten en de cutscene characters aan te zetten
        yield return new WaitUntil(() => timerCutscene >= 1.5f);

        if (!cutscenePart1)
        {
            char1.SetActive(false);
            char2.SetActive(false);
            char3.SetActive(false);
            char4.SetActive(false);
            cutsceneCharacters.SetActive(true);
            cutscenePart1 = true;
        }

        yield return new WaitUntil(() => timerCutscene >= 4f);

        if (!cutscenePart2)
        {
            StartCoroutine(LightFlashCoroutine());
            shot.Play();
            cutscenePart2 = true;
        }

        yield return new WaitUntil(() => timerCutscene >= 5f);

        if (!cutscenePart3)
        {
            StartCoroutine(ScaleBloodPool(bloodpool));
            bloodpool.SetActive(true);
            SayDeathLine.Invoke();
            cutscenePart3 = true;
        }

        yield return new WaitUntil(() => timerCutscene >= 12.97f);

        StartCoroutine(LightFlashCoroutine());

        yield return new WaitUntil(() => timerCutscene >= 13f);

        if (!cutscenePart4)
        {
            // Maak het scherm zwart
            animator.SetTrigger("Dead");
            shot.pitch = 0.98f;
            shot.Play();
            RainVolumeDown.Invoke();
            cutscenePart4 = true;
        }

        yield return new WaitUntil(() => timerCutscene >= 17f);

        // Laad de volgende scene in
        if (!cutscenePart5)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
            cutscenePart5 = true;
        }
    }

    // Functie die het bloed (de cylinder) vergroot
    private IEnumerator ScaleBloodPool(GameObject cylinder)
    {
        Transform cylinderTransform = cylinder.transform;
        Vector3 initialScale = cylinderTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < bloodpoolDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / bloodpoolDuration;

            cylinderTransform.localScale = Vector3.Lerp(initialScale, bloodTargetScale, t);

            yield return null;
        }

        cylinderTransform.localScale = bloodTargetScale;
    }
}
