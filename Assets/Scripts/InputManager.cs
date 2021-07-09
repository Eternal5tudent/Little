using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    public Vector2 AxisInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool TalkInput { get; private set; }
    public bool GrabWallToggle { get; private set; } = false;
    public bool IsPointerOverUI { get { return EventSystem.current.IsPointerOverGameObject(); } }

    private void Update()
    {
        AxisInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(PressedJump());
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GrabWallToggle = !GrabWallToggle;
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(PressedTalk());
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

    private IEnumerator PressedTalk()
    {
        TalkInput = true;
        Debug.Log(TalkInput);
        yield return null;
        TalkInput = false;
    }
}
