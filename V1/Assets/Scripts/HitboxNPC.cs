using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wordt ook niet meer gebruikt
// Om te testen of ik de rotatie van een child uit kan zetten
public class HitboxNPC : MonoBehaviour
{
    public Transform npc;
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        transform.position = npc.position;

        transform.rotation = Quaternion.identity;
    }
}
