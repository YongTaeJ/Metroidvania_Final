using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public int NormInputX {  get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();

        NormInputX = (int)(Movement * Vector2.right).normalized.x;
        NormInputY = (int)(Movement * Vector2.up).normalized.y;

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }

    public void UseJumpInput()
    {
        JumpInput = false;
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
