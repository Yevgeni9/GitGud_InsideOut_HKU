using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

// Script is voor Dialoog in scene 2
// Tekens worden een voor een onthuld
// Lines krijgen hun eigen kleur, pitch en speed (om personages te onderscheiden)
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float defaultTextSpeed;
    public AudioSource typingSound;
    public AudioSource DoorBonk;

    public UnityEvent CampfireOut;

    private int index;
    private string currentLine;
    private float currentTextSpeed;

    [System.Serializable]
    public struct PitchRange
    {
        public float minPitch;
        public float maxPitch;
    }

    public PitchRange[] pitchRanges;

    public Color color1 = Color.blue;
    public Color color2 = Color.green;
    public Color color3 = Color.red;
    public List<int> color1Indices = new List<int> { 0, 1 };
    public List<int> color2Indices = new List<int> { 2, 3 };
    public List<int> color3Indices = new List<int> { 4, 5 };

    public Animator animator;

    public Image icon;

    void Start()
    {
        textComponent.text = string.Empty;
        currentTextSpeed = defaultTextSpeed;
        Invoke("StartDialogue", 5f);
        icon.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (textComponent.text == currentLine)
        {
            icon.enabled = true;
        }
        else
        {
            icon.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == currentLine)
            {
                NextLine();
            }
        }

        if (index == 11)
        {
            DoorBonk.Play();
        }

        if (index == 15)
        {
            Debug.Log("TRIGGERED");
            StartCoroutine(WaitForFade(1.5f));
            StartCoroutine(LoadScene(8f));
            CampfireOut.Invoke();
        }
    }

    private IEnumerator WaitForFade(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("PlayAnimation");
    }

    private IEnumerator LoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartDialogue()
    {
        index = 0;
        currentLine = ParseText(lines[index]);
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {

        string rawLine = lines[index];
        textComponent.text = string.Empty;
        currentTextSpeed = defaultTextSpeed;

        Color currentColor;

        if (color1Indices.Contains(index))
        {
            currentColor = color1;
        }
        else if (color2Indices.Contains(index))
        {
            currentColor = color2;
        }
        else if (color3Indices.Contains(index))
        {
            currentColor = color3;
        }
        else
        {
            currentColor = color2;
        }

        textComponent.color = currentColor;

        PitchRange currentPitchRange = pitchRanges[index];

        for (int i = 0; i < rawLine.Length; i++)
        {
            char c = rawLine[i];

            if (c == '{' && rawLine.Substring(i).StartsWith("{speed="))
            {
                int end = rawLine.IndexOf('}', i);
                string speedValue = rawLine.Substring(i + 7, end - (i + 7));
                if (float.TryParse(speedValue, out float newSpeed))
                {
                    currentTextSpeed = newSpeed;
                }
                i = end;
                continue;
            }

            textComponent.text += c;

            if (typingSound != null)
            {
                typingSound.pitch = Random.Range(currentPitchRange.minPitch, currentPitchRange.maxPitch);
                typingSound.Play();
            }

            yield return new WaitForSeconds(currentTextSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            currentLine = ParseText(lines[index]);
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }

    string ParseText(string line)
    {
        while (line.Contains("{speed="))
        {
            int start = line.IndexOf("{speed=");
            int end = line.IndexOf('}', start);
            if (start != -1 && end != -1)
            {
                line = line.Remove(start, (end - start) + 1);
            }
        }
        return line;
    }
}
