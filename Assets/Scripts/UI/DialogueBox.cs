using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dialogue_Udemy;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [Range(0.01f, 0.1f)]
    [SerializeField] float newCharEverySeconds = 0.5f;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] Button answerButtonPrefab;
    [SerializeField] GameObject possibleAnswers;
    [SerializeField] AudioClip typeSound;

    private Dialogue currentDialogue;
    private DialogueNode currentNode;
    private string currentSentence;
    AudioManager audioManager;

    private Animator animator;

    private void Awake()
    {
        NPC.OnTalk += OnNPCTalk;
        NPC.OnPlayerLeft += CloseBox;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = AudioManager.Instance;
        animator.SetBool("open", false);
    }

    private void OnNPCTalk(Dialogue dialogue)
    {
        if (currentDialogue == null)
        {
            ChangeDialogue(dialogue);
            RenderCurrentSentence();
        }
    }

    private void ChangeDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentNode = currentDialogue.GetFirstNode();
        animator.SetBool("open", true);
    }

    public void RenderCurrentSentence()
    {
        currentSentence = currentNode.GetText();
        dialogueText.text = currentSentence;
        StartCoroutine(DisplayText_Cor(currentSentence));
    }

    public void ChooseAnswer(DialogueNode answerNode)
    {
        currentNode = currentDialogue.GetNextNode(answerNode);
        RenderCurrentSentence();
    }

    void DeleteExistingAnswerButtons()
    {
        for (int i = 0; i < possibleAnswers.transform.childCount; i++)
        {
            Destroy(possibleAnswers.transform.GetChild(i).gameObject);
        }
    }

    public void RenderCurrentAnswers()
    {
        void TryCloseBox()
        {
            CloseBox();
        }
        int answerNum = 0;
        foreach (DialogueNode answerNode in currentDialogue.GetAllChildren(currentNode))
        {
            if (answerNode.IsPlayerSpeaking())
            {
                string answer = answerNode.GetText();
                Button button = Instantiate(answerButtonPrefab, possibleAnswers.transform);
                button.onClick.AddListener(() => { ChooseAnswer(answerNode); });
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = answer;
                answerNum++;
            }
        }
        if (answerNum == 0) // Player has no answer
        {
            if (currentNode.ChildCount > 0) // if there is another node to display
            {
                DialogueNode answerNode = currentDialogue.GetNextNode(currentNode);
                Button button = Instantiate(answerButtonPrefab, possibleAnswers.transform);
                button.onClick.AddListener(() => { ChooseAnswer(currentNode); });
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = "...";
            }
            else // if this is a closing node
            {
                Button button = Instantiate(answerButtonPrefab, possibleAnswers.transform);
                button.onClick.AddListener(() => { TryCloseBox(); });
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Close";
            }

        }
    }

    IEnumerator DisplayText_Cor(string sentence)
    {
        DeleteExistingAnswerButtons();
        char[] characters = sentence.ToCharArray();
        dialogueText.maxVisibleCharacters = 0;
        foreach (char letter in characters)
        {
            audioManager.PlaySFX(typeSound);
            dialogueText.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(newCharEverySeconds);
        }
        RenderCurrentAnswers();
    }

    public void CloseBox()
    {
        StopAllCoroutines();
        currentDialogue = null;
        currentNode = null;
        currentSentence = null;
        animator.SetBool("open", false);
    }
}
