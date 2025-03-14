using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script is voor de enemies die rond moeten lopen tussen de afgestelde waypoints
// Kan zelf waypoints toevoegen
public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float waitTime = 1f;
    public Animator characterAnimator;
    private int currentWaypointIndex = 0;
    private bool isWalking = true;
    private bool isWaiting = false;

    void Start()
    {
        if (characterAnimator == null)
            characterAnimator = GetComponent<Animator>();

        UpdateAnimationState();
    }

    void Update()
    {
        if (!isWaiting)
        {
            PatrolFunct();
        }
    }

    void PatrolFunct()
    {
        if (waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        Vector3 direction = targetWaypoint.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (direction.magnitude > 0.1f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }

        isWalking = direction.magnitude > 0.1f;
        UpdateAnimationState();
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;

        isWalking = false;
        UpdateAnimationState();

        yield return new WaitForSeconds(waitTime);

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        isWaiting = false;
    }

    void UpdateAnimationState()
    {
        if (isWaiting)
        {
            isWalking = false;
        }
        else
        {
            isWalking = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) > 0.1f;
        }

        characterAnimator.SetBool("IsWalking", isWalking);
    }
}
