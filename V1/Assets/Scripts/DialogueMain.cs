using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Script is een aangepast versie van Dialogue.cs voor scene 3
// Dialoog wordt nu weergeven aan de hand van de positie van de speler op de z-as, in plaats van met spatie
public class DialogueMain : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float defaultTextSpeed;
    public AudioSource typingSound;

    private int index;
    private string currentLine;
    private float currentTextSpeed;

    private bool Testbool3 = false;

    public Transform player;

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

    public float[] zPositions = { -22f, -5f, 50f };
    private bool[] linesShown;
    private float waitTime = 3f;

    void Start()
    {
        textComponent.text = string.Empty;
        currentTextSpeed = defaultTextSpeed;
        Invoke("StartDialogue", 1.5f);
        linesShown = new bool[zPositions.Length];
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Debug.Log(index);
        for (int i = 0; i < zPositions.Length; i++)
        {
            if (player.position.z > zPositions[i] && !linesShown[i])
            {
                if (textComponent.text == currentLine)
                {
                    NextLine();
                }

                else
                {
                    StartCoroutine(WaitAndNextLine());
                }

                linesShown[i] = true;
            }
        }
    }

    private IEnumerator WaitAndNextLine()
    {
        yield return new WaitForSeconds(waitTime);
        NextLine();
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

    public void OnCutscene()
    {
        index = 19;
        if (!Testbool3)
        {
            NextLine();
            Testbool3 = true;
        }
    }
}
