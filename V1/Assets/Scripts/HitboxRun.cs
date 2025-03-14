using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Script regelt bepaalde scripted stukken zoals de twee rennende mannen en de laatste patrol
// Gebruikt ook de positie van de speler op de z-as om dit te doen
public class HitboxRun : MonoBehaviour
{
    public Transform player;
    public GameObject character1;
    public GameObject character2;
    public float moveDistance = 2.0f;
    public float moveSpeed = 2.0f;
    public float moveSpeed2 = 2.0f;
    public GameObject StartPatrol;
    public GameObject StartPatrol2;
    public float PatrolStartZ;
    public UnityEvent Shiftcamera;

    public float zValue = 0;
    private bool Patrolstarted = false;

    private Vector3 character1StartPos;
    private Vector3 character2StartPos;
    private bool shouldMove = false;

    void Start()
    {
        StartPatrol.SetActive(false);
        StartPatrol2.SetActive(false);
        if (character1 != null)
            character1StartPos = character1.transform.position;

        if (character2 != null)
            character2StartPos = character2.transform.position;
    }

    void Update()
    {
        if (player.position.z > zValue && !shouldMove)
        {
            shouldMove = true;
        }

        if (shouldMove)
        {
            MoveCharacter(character1, character1StartPos);
            MoveCharacter2(character2, character2StartPos);
        }

        if (player.position.z > PatrolStartZ && !Patrolstarted)
        {
            Patrolstarted = true;
            StartPatrol.SetActive (true);
            Shiftcamera.Invoke();
            StartPatrol2.SetActive (true);
        }
    }

    private void MoveCharacter(GameObject character, Vector3 startPos)
    {
        if (character == null) return;

        Vector3 targetPosition = startPos + Vector3.back * moveDistance;
        character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (character.transform.position == targetPosition)
        {
            shouldMove = false;
        }
    }

    private void MoveCharacter2(GameObject character, Vector3 startPos)
    {
        if (character == null) return;

        Vector3 targetPosition = startPos + Vector3.back * moveDistance;
        character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, moveSpeed2 * Time.deltaTime);

        if (character.transform.position == targetPosition)
        {
            shouldMove = false;
        }
    }
}
