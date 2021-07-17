using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    const float dashTime = 0.1f;
    const float holdJumpTime = 0.25f;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float timePassed = Time.time - startTime;
        if(timePassed <= dashTime) // if dashing
        {
            Dash();
        }
        else // after dashing
        {
            if (player.AxisInput.x * player.FacingDirection == -1)
            {
                player.Flip();
            }
            if (player.InputHandler.JumpHold && timePassed <= holdJumpTime)
            {
                player.SetVelocityY(playerData.wallJumpForce);
            }
            if(player.IsTouchingWall && !player.InputHandler.JumpHold)
            {
                isAbilityDone = true;
            }
            player.ControlPlayer(60);
        }
        if(timePassed > holdJumpTime || player.IsGrounded)
        {
            isAbilityDone = true;
        }
    }

    private void Dash()
    {
        player.SetVelocity(new Vector2(player.FacingDirection * 10, playerData.wallJumpForce));
    }
}
