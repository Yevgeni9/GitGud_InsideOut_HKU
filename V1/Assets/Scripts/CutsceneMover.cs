using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Omdat je op verschillende plekken gedecteerd kan worden moet de cutscene mee verplaats worden
// Ik had de cutscene ook op de speler kunnen zetten, maar dat kan voor visuele bugs zorgen
public class CutsceneMover : MonoBehaviour
{
    public Transform player;
    public float[] zPositions;
    public GameObject cutsceneObject;

    void Update()
    {
        if (player != null && zPositions.Length > 0 && cutsceneObject != null)
        {
            float playerZ = player.position.z;

            float closestZ = zPositions[0];
            float smallestDistance = Mathf.Abs(playerZ - closestZ);

            foreach (float z in zPositions)
            {
                float distance = Mathf.Abs(playerZ - z);
                if (distance < smallestDistance)
                {
                    closestZ = z;
                    smallestDistance = distance;
                }
            }

            Vector3 newPosition = cutsceneObject.transform.position;
            newPosition.z = closestZ;
            cutsceneObject.transform.position = newPosition;
            Debug.Log($"Cutscene is now at Z: {closestZ}");
        }
    }
}
