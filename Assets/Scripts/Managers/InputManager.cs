using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    public Vector2 AxisInput { get; private set; }
    /// <summary>
    /// Don't forget to always use "ConsumeJump()" function
    /// </summary>
    public bool JumpDown { get; private set; }
    public bool JumpHold { get; private set; }
    public bool TalkInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool GrabWallToggle { get; private set; } = false;
    public bool IsPointerOverUI { get { return EventSystem.current.IsPointerOverGameObject(); } }

    private bool jumpUsed = false;


    private void Update()
    {
        AxisInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(PressedJump());
            JumpHold = true;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            JumpHold = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GrabWallToggle = !GrabWallToggle;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(PressedTalk());
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(PressedFire());
        }
    }

    public void ResetWallGrab()
    {
        GrabWallToggle = false;
    }

    private IEnumerator PressedJump()
    {
        JumpDown = true;
        jumpUsed = false;
        float startTime = Time.time;
        while (Time.time < startTime + 0.2f)
        {
            if (jumpUsed)
                JumpDown = false;
            yield return new WaitForEndOfFrame();
        }
        JumpDown = false;
    }

    public void ConsumeJump()
    {
        jumpUsed = true;
    }

    private IEnumerator PressedTalk()
    {
        TalkInput = true;
        yield return null;
        TalkInput = false;
    }

    private IEnumerator PressedFire()
    {
        FireInput = true;
        yield return null;
        FireInput = false;
    }
}
