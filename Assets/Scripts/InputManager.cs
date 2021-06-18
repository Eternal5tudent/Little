using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Vector2 AxisInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool GrabWallToggle { get; private set; } = false;

    private void Update()
    {
        AxisInput = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
        if (UnityEngine.Input.GetButtonDown("Jump"))
        {
            StartCoroutine(PressedJump());
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
        {
            GrabWallToggle = !GrabWallToggle;
        }
    }

    public void ResetWallGrab()
    {
        GrabWallToggle = false;
    }

    private IEnumerator PressedJump()
    {
        JumpInput = true;
        yield return new WaitForSeconds(0.1f);
        JumpInput = false;
    }
}
