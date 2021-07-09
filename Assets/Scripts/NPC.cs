using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue_Udemy;
using System;

public class NPC : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    private bool shouldTalk = false;
    public static Action<Dialogue> OnTalk;
    public static Action OnPlayerLeft;

    public void Talk()
    {
        OnTalk?.Invoke(dialogue);
    }

    private void Update()
    {
        if(shouldTalk)
        {
            if(InputManager.Instance.TalkInput) //todo: create a seperate key for this functionallity
            {
                Debug.Log("HAHAHAHA");
                Talk();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            shouldTalk = true;
            Debug.Log("PLAYER DETECTION");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            shouldTalk = false;
            OnPlayerLeft?.Invoke();
        }
    }
}
