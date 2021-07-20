using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_TestScene : MonoBehaviour
{
    [SerializeField] AudioClip music;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(music);
    }
}
