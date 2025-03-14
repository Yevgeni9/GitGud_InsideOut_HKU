using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Voor de menu knoppen
// Start ook de volgende scene aan de hand van de knop
public class Menu : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public void StartButton()
    {
        Debug.Log("Works");
        StartCoroutine(LoadScene(4f));
        audioSource.Play();
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
        audioSource.Play();
    }

    private IEnumerator LoadScene(float delay)
    {
        animator.SetTrigger("PlayAnimation");
        yield return new WaitForSeconds(delay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
