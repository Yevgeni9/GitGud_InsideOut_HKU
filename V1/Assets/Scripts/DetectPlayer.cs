using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Voor het detecteren van de speler, wordt gereset als je binnen de tijd de collider uitloopt
public class DetectPlayer : MonoBehaviour
{
    public float detectionTime = 1.0f;
    private float timer = 0.0f;
    private bool isPlayerInTrigger = false;
    public UnityEvent StartCutscene;
    public UnityEvent DisableControls;

    // Niet weghalen
    private bool test1 = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            timer = 0.0f;
        }
    }

    void Update()
    {
        if (isPlayerInTrigger)
        {
            timer += Time.deltaTime;

            if (timer >= detectionTime && !test1)
            {
                StartCutscene.Invoke();
                DisableControls.Invoke();
                Debug.Log("Speler gedetecteerd!");
                test1 = true;
            }
        }
    }
}
