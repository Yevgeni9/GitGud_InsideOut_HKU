using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gebruik ik niet meer
public class TRest : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
