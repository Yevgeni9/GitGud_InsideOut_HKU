using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorEnable : MonoBehaviour
{
    void Start()
    {
        // Verberg de cursor
        Cursor.visible = true;

        // Zet de cursor vast in het midden van het scherm
        Cursor.lockState = CursorLockMode.None;
    }
}
