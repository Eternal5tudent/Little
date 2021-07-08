using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dialogue_Udemy;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueText;
    //todo: temporary
    [SerializeField] Dialogue currentDialogue;
    [SerializeField] GameObject possibleAnswers;
    [SerializeField] Button answerButtonPrefab;
    DialogueNode currentNode;
    string currentSentence;

    private Animator animator;

    private void Start()
    {
        currentNode = currentDialogue.GetFirstNode();
        currentSentence = currentNode.GetText();
        dialogueText.text = currentSentence;
        StartCoroutine(DisplayText_Cor(currentSentence));
        RenderAnswers();
        animator = GetComponent<Animator>();
        animator.SetBool("open", true);
    }

    public void ChangeSentence()
    {
        currentSentence = currentNode.GetText();
        dialogueText.text = currentSentence;
        StartCoroutine(DisplayText_Cor(currentSentence));
        RenderAnswers();//
    }

    public void ChooseAnswer(DialogueNode answerNode)
    {
        currentNode = currentDialogue.GetNextNode(answerNode);
        ChangeSentence();
    }

    public void RenderAnswers()
    {
        void DeleteExistingAnswerButtons()
        {
            for (int i = 0; i < possibleAnswers.transform.childCount; i++)
            {
                Destroy(possibleAnswers.transform.GetChild(i).gameObject);
            }
        }

        DeleteExistingAnswerButtons();
        int answerNum = 0;
        foreach(DialogueNode answerNode in currentDialogue.GetAllChildren(currentNode))
        {
            if(answerNode.IsPlayerSpeaking())
            {
                string answer = answerNode.GetText();
                Button button = Instantiate(answerButtonPrefab, possibleAnswers.transform);
                button.onClick.AddListener(() => { ChooseAnswer(answerNode); });
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = answer;
                answerNum++;
            }
        }
        if(answerNum == 0) // Player has no answer
        {
            if(currentNode.ChildCount > 0) // if there is another node to display
            {
                DialogueNode answerNode = currentDialogue.GetNextNode(currentNode);
                Button button = Instantiate(answerButtonPrefab, possibleAnswers.transform);
                button.onClick.AddListener(() => { ChangeSentence(); });
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = "...";
            }
            else // if this is a closing node
            {
                Button button = Instantiate(answerButtonPrefab, possibleAnswers.transform);
                button.onClick.AddListener(() => { CloseBox(); });
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Close";
            }
           
        }
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

    public void CloseBox()
    {
        Debug.Log("Closing Dialogue Box");
        animator.SetBool("open", false);
    }
}
