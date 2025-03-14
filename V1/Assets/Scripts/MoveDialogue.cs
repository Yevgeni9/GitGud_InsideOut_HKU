using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Onhandig script voor het bewegen van de dialogue box in scene 3
// Gebruikt voorafgestelde posities om dit te doen (ik ga dit nooit meer opnieuw gebruiken)
public class MoveDialogue : MonoBehaviour
{
    [SerializeField] private GameObject border;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject text;

    public Transform player;
    private float fix = 2.2f;

    public float[] moveDown = { 45f, 150f, 250f };
    public float[] moveUp = { 2f, 100f, 200f };

    public float moveDuration = 1f;
    public float moveSpeed = 2f;

    private float[] moveStartTime;
    private bool[] isMovingUp;
    private bool[] isMovingDown;
    private bool[] hasMovedUp;
    private bool[] hasMovedDown;

    void Start()
    {
        moveStartTime = new float[moveUp.Length];
        isMovingUp = new bool[moveUp.Length];
        isMovingDown = new bool[moveDown.Length];
        hasMovedUp = new bool[moveUp.Length];
        hasMovedDown = new bool[moveDown.Length];
    }

    void Update()
    {
        for (int i = 0; i < moveUp.Length; i++)
        {
            if (player.position.z > moveUp[i] && !isMovingUp[i] && !hasMovedUp[i])
            {
                StartMovingUp(i);
            }
        }

        for (int i = 0; i < moveDown.Length; i++)
        {
            if (player.position.z > moveDown[i] && !isMovingDown[i] && !hasMovedDown[i])
            {
                StartMovingDown(i);
            }
        }

        MoveObjects();
    }

    private void StartMovingUp(int index)
    {
        isMovingUp[index] = true;
        moveStartTime[index] = Time.time;
    }

    private void StartMovingDown(int index)
    {
        isMovingDown[index] = true;
        moveStartTime[index] = Time.time;
    }

    private void MoveObjects()
    {
        for (int i = 0; i < moveUp.Length; i++)
        {
            if (isMovingUp[i])
            {
                float t = (Time.time - moveStartTime[i]) / moveDuration;
                float moveAmount = Mathf.Lerp(0, 1, t);

                // Beweeg objecten omhoog
                border.transform.position += Vector3.up * moveAmount * moveSpeed * Time.deltaTime * fix;
                box.transform.position += Vector3.up * moveAmount * moveSpeed * Time.deltaTime * fix;
                text.transform.position += Vector3.up * moveAmount * moveSpeed * Time.deltaTime * fix;

                if (t >= 1f)
                {
                    isMovingUp[i] = false;
                    hasMovedUp[i] = true;
                }
            }

            if (isMovingDown[i])
            {
                float t = (Time.time - moveStartTime[i]) / moveDuration;
                float moveAmount = Mathf.Lerp(0, 1, t);

                border.transform.position -= Vector3.up * moveAmount * moveSpeed * Time.deltaTime * fix;
                box.transform.position -= Vector3.up * moveAmount * moveSpeed * Time.deltaTime * fix;
                text.transform.position -= Vector3.up * moveAmount * moveSpeed * Time.deltaTime * fix;

                if (t >= 1f)
                {
                    isMovingDown[i] = false;
                    hasMovedDown[i] = true;
                }
            }
        }
    }
}