using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    const float dashTime = 0.17f;
    const float holdJumpTime = 0.3f;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Flip();
        Debug.Log("DASH!!");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float timePassed = Time.time - startTime;
        if (timePassed <= dashTime) // Dash Phase
        {
            Dash();
        }
        else if (timePassed <= holdJumpTime) // Hold/Control Phase
        {
            if (player.AxisInput.x * player.FacingDirection == -1)
            {
                player.Flip();
            }
            if (player.InputHandler.JumpHold && !Transitioning)
            {
                player.SetVelocityY(playerData.wallJumpForce);
            }
            else if (player.IsTouchingWall && !player.InputHandler.JumpHold)
            {
                isAbilityDone = true;
            }
            else if (player.IsGrounded)
            {
                isAbilityDone = true;

            }
            else if (!player.InputHandler.JumpHold)
            {
                player.SetVelocityY(0);
                isAbilityDone = true;
            }
            if (!Transitioning && !player.IsTouchingWall)
                player.ControlPlayer();
        }
        else
        {
            isAbilityDone = true;
        }

    }

    private void Dash()
    {
        player.SetVelocity(new Vector2(player.FacingDirection * 5, playerData.wallJumpForce));
    }
}
