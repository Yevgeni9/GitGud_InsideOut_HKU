using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script start een animatie wanneer de speler de trigger inloopt
// Wordt gebruikt voor de boot wegsturen wanneer deze in beeld is
public class BoatGoes : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("HitTrigger");
        }
    }
}
