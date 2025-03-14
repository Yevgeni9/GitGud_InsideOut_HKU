using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

// Script is voor de (mini)cutscene als je gedetecteerd wordt
// Gedaan via code want Timelines in Unity zijn verwarrend
// (Voor de refactor) Ik had niet al te veel tijd om dit te maken en het is super slordig, daarom heb ik ervoor gekozen dit script te verbeteren
// (Voor de refactor) Dit is de niet-verbeterde versie, die ik heb gebruikt voor mijn game in project Inside Out

public class CutsceneV1 : MonoBehaviour
{
    public GameObject char1;
    public GameObject char2;

    public UnityEvent SayLine;
    public UnityEvent RainDown;

    public AudioSource Alarm;
    public AudioSource Shot;

    private float timerCutscene = 0f;
    public Animator animator;
    public GameObject Cutscene1;
    public GameObject Rain;

    // Dit is een puinhoop
    private bool timerStarted = false;
    private bool Testbool = false;
    private bool Testbool2 = false;
    private bool Testbool4 = false;
    private bool Testbool5 = false;
    private bool Testbool6 = false;
    private bool actionExecuted = false;

    public GameObject char3;
    public GameObject char4;

    public Vector3 targetScale = new Vector3(2f, 0.05f, 2f);
    public float duration = 5f;

    public GameObject Cylinder;

    public Light muzzleFlashLight;
    public float flashDuration = 0.1f;


    void Start()
    {
        Cutscene1.SetActive(false);
        muzzleFlashLight.enabled = false;
    }

    void Update()
    {
        if (timerStarted)
        {
            timerCutscene += Time.deltaTime;
        }
    }

    public void StartCutscene()
    {
        timerStarted = true;
        animator.SetTrigger("PlayerDetected");
        Cylinder.SetActive(false);

        Debug.Log("Cutscene started");

        if (!Testbool)
        {
            Alarm.Play();
            Testbool = true;
            Debug.Log("Alarm started");
        }
        StartCoroutine(CutsceneTimer());
    }

    private IEnumerator LightFlashCoroutine()
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        muzzleFlashLight.enabled = false;
    }
    private IEnumerator CutsceneTimer()
    {
        yield return new WaitUntil(() => timerCutscene >= 1.5f);

        if (!actionExecuted)
        {
            actionExecuted = true;
            Debug.Log("Players should be removed");
            char1.SetActive(false);
            char2.SetActive(false);
            char3.SetActive(false);
            char4.SetActive(false);
            Cutscene1.SetActive(true);
        }

        yield return new WaitUntil(() => timerCutscene >= 4f);

        if (!Testbool4)
        {
            Shot.Play();
            StartCoroutine(LightFlashCoroutine());
            Testbool4 = true;
            
        }

        yield return new WaitUntil(() => timerCutscene >= 5f);

        if (!Testbool5)
        {
            Cylinder.SetActive(true);
            StartCoroutine(ScaleOverTime(Cylinder));
            SayLine.Invoke();
            Testbool5 = true;
        }

        yield return new WaitUntil(() => timerCutscene >= 12.97f);

        StartCoroutine(LightFlashCoroutine());

        yield return new WaitUntil(() => timerCutscene >= 13f);

        animator.SetTrigger("Dead");

        if (!Testbool2)
        {
            Shot.pitch = 0.98f;
            Shot.Play();
            
            Testbool2 = true;
            RainDown.Invoke();
        }

        yield return new WaitUntil(() => timerCutscene >= 17f);

        if (!Testbool6)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
            Testbool6 = true;
        }
    }

    private IEnumerator ScaleOverTime(GameObject cylinder)
    {
        Transform cylinderTransform = cylinder.transform;
        Vector3 initialScale = cylinderTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            cylinderTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null;
        }

        cylinderTransform.localScale = targetScale;
    }
}
