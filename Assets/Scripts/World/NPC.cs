using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue_Udemy;
using System;

public class NPC : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    public static Action<Dialogue> OnTalk;

    public void Talk()
    {
        OnTalk?.Invoke(dialogue);
    }

    public void Interact()
    {
        Talk();
    }

}
