using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script zorgt ervoor dat Luna je volgt
public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 2f;
    public float followDistance = 3f;
    public Animator characterAnimator;

    private Vector3 previousPosition;
    private Vector3 targetPosition;
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        targetPosition = player.position - player.forward * followDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        transform.LookAt(player);

        Vector3 movementDirection = (transform.position - previousPosition).normalized;
        float speed = (transform.position - previousPosition).magnitude / Time.deltaTime;

        UpdateAnimations(movementDirection, speed);

        previousPosition = transform.position;
    }

    void UpdateAnimations(Vector3 movementDirection, float speed)
    {
        Vector3 localDirection = player.InverseTransformDirection(movementDirection);

        bool isWalking = speed > 0.4f && speed <= 2.25f;
        bool isRunning = speed > 2.25f;

        bool isWalkingRight = false;
        bool isWalkingLeft = false;
        bool isWalkingBack = false;

        if (localDirection.z > 0.1f)
        {
            isWalkingRight = isWalking;
            isRunning = speed > 2.25f;
        }
        else if (localDirection.z < -0.1f)
        {
            isWalkingBack = isWalking;
        }
        else if (localDirection.x > 0.1f)
        {
            isWalkingRight = isWalking;
        }
        else if (localDirection.x < -0.1f)
        {
            isWalkingLeft = isWalking;
        }

        characterAnimator.SetBool("IsWalkingRight", isWalkingRight);
        characterAnimator.SetBool("IsWalkingBack", isWalkingBack);
        characterAnimator.SetBool("IsRunningRight", isRunning);
    }
}