using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] AudioClip pressSound;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { AudioManager.Instance.PlaySFX(pressSound); });
    }
}
