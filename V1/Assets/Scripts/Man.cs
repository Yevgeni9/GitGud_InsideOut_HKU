using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animaties voor de speler beheren
public class Man : MonoBehaviour
{
    public Animator characterAnimator;
    public Man2 movementScript;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        movementScript = GetComponent<Man2>();
    }

    void Update()
    {
        KeyCode activeKey = movementScript.activeKey;

        bool isWalkingRight = activeKey == KeyCode.D && Input.GetKey(KeyCode.D);
        bool isRunningRight = isWalkingRight && Input.GetKey(KeyCode.LeftShift);
        bool isWalkingBack = activeKey == KeyCode.A && Input.GetKey(KeyCode.A);

        characterAnimator.SetBool("IsWalkingRight", isWalkingRight);
        characterAnimator.SetBool("IsRunningRight", isRunningRight);
        characterAnimator.SetBool("IsWalkingBack", isWalkingBack);
    }
}