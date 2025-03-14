using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script beweegt de speler via toetsenbord inputs
public class Man2 : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float runSpeed = 2.1f;
    public float backSpeed = 1.0f;

    public MonoBehaviour scriptToDisable;

    public KeyCode activeKey = KeyCode.None;

    void Update()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        if (activeKey == KeyCode.None)
        {
            if (Input.GetKey(KeyCode.D))
            {
                activeKey = KeyCode.D;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                activeKey = KeyCode.A;
            }
        }

        if (activeKey == KeyCode.D && Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            Debug.Log(Input.GetKey(KeyCode.LeftShift) ? "Running" : "Walking");
        }
        else if (activeKey == KeyCode.A && Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * backSpeed * Time.deltaTime);
            Debug.Log(Input.GetKey(KeyCode.LeftShift) ? "Running" : "Walking");
        }

        if (activeKey == KeyCode.D && !Input.GetKey(KeyCode.D))
        {
            activeKey = KeyCode.None;
        }
        else if (activeKey == KeyCode.A && !Input.GetKey(KeyCode.A))
        {
            activeKey = KeyCode.None;
        }
        Debug.Log(Input.GetKey(KeyCode.LeftShift) ? "Running" : "Walking");
    }

    // Voor als je de cutscene ingaat
    public void DisableMovement()
    {
        scriptToDisable.enabled = false;
    }
}
