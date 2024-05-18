using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogs : MonoBehaviour
{
    public TextMeshProUGUI TextDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject Panel;

    private void Start()
    {
        TextDisplay.text = string.Empty;
    }
    public void StartConversation()
    {
        Panel.SetActive(true);
        index = 0;
        StartCoroutine(Conversation());
    }

    public void Next()
    {
        if (TextDisplay.text == sentences[index]) NextSentence();
        else
        {
            StopAllCoroutines();
            TextDisplay.text = sentences[index];
        }
    }
    IEnumerator Conversation()
    {

        foreach (char i in sentences[index].ToCharArray())
        {
            TextDisplay.text += i.ToString();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            TextDisplay.text = string.Empty;
            StartCoroutine(Conversation());
        }
        else
        {
            Panel.SetActive(false);
            TextDisplay.text = string.Empty;
            index = 0;
        }
    }
}

