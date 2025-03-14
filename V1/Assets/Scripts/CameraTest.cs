using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script laat de camera de speler volgen met een voorafbepaalde offset
// Past ook de FOV aan voor een gedeelte van de game
public class CameraTest : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, player.position.z) + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }

    public void ShiftcameraSmoothWithFOV(Vector3 targetOffset, float targetFOV, float duration)
    {
        StartCoroutine(AdjustOffsetAndFOVSmoothly(targetOffset, targetFOV, duration));
    }

    private IEnumerator AdjustOffsetAndFOVSmoothly(Vector3 targetOffset, float targetFOV, float duration)
    {
        Vector3 initialOffset = offset;
        float initialFOV = cam.fieldOfView;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            offset = Vector3.Lerp(initialOffset, targetOffset, elapsedTime / duration);

            cam.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        offset = targetOffset;
        cam.fieldOfView = targetFOV;
    }

    public void ShiftSmooth()
    {
        ShiftcameraSmoothWithFOV(new Vector3(0, 0, 0), 27f, 3f);
    }
}
