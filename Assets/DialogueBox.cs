using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dialogue_Udemy;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueText;
    //todo: temporary
    [SerializeField] Dialogue currentDialogue;
    DialogueNode currentNode;
    string currentSentence;

    private void Start()
    {
        currentNode = currentDialogue.GetFistNode();
        currentSentence = currentNode.GetText();
        dialogueText.text = currentSentence;
        StartCoroutine(DisplayText_Cor(currentSentence));

    }

    public void NextSentence()
    {
    }

    public void ChooseAnswer()
    {

    }

    IEnumerator DisplayText_Cor(string sentence)
    {
        char[] characters = sentence.ToCharArray();
        dialogueText.maxVisibleCharacters = 0;
        foreach (char letter in characters)
        {
            print("hey");
            dialogueText.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(0.04f);
        }
    }
}
