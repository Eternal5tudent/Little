using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Vector2 Input { get; private set; }
    public bool JumpInput { get; private set; }

    private void Update()
    {
        Input = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
        if (UnityEngine.Input.GetButtonDown("Jump"))
        {
            StartCoroutine(PressedJump());
        }
    }

    private IEnumerator PressedJump()
    {
        JumpInput = true;
        yield return new WaitForSeconds(0.4f);
        JumpInput = false; 
    }
}
