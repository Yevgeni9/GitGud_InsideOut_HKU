using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script is voor de controls scene
// Laad ook de volgende scene als alle inputs ingedrukt zijn
public class Checkboxes : MonoBehaviour
{
    public Toggle spaceToggle;
    public Toggle aToggle;
    public Toggle dToggle;
    public Toggle dShiftToggle;

    public Animator animator;
    public AudioSource Checked;

    private void Start()
    {
        spaceToggle.isOn = false;
        aToggle.isOn = false;
        dToggle.isOn = false;
        dShiftToggle.isOn = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !spaceToggle.isOn)
        {
            spaceToggle.isOn = true;
            Checked.Play();
        }

        if (Input.GetKeyDown(KeyCode.A) && !aToggle.isOn)
        {
            aToggle.isOn = true;
            Checked.Play();
        }

        if (Input.GetKeyDown(KeyCode.D) && !dToggle.isOn)
        {
            dToggle.isOn = true;
            Checked.Play();
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && !dShiftToggle.isOn)
        {
            dShiftToggle.isOn = true;
            Checked.Play();
        }

        if (dShiftToggle.isOn && spaceToggle.isOn && aToggle.isOn && dToggle.isOn)
        {
            animator.SetTrigger("PlayAnimation");
            StartCoroutine(LoadScene(3f));
        }
    }

    private IEnumerator LoadScene(float delay)
    {
        yield return new WaitForSeconds(delay); 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
