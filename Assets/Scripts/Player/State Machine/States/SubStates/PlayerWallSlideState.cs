using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerWallState
{
    int slideAudioId;
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        slideAudioId = player.audioManager.GetUniqueAudioID();
        player.audioManager.PlaySFXLooped(playerData.slide, slideAudioId);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGrounded && !player.GrabToggled)
        {
            stateMachine.ChangeState(player.IdleState);
            player.InputHandler.ResetWallGrab();
        }

        if(!(player.AxisInput.y < 0) || player.GrabToggled)
        {
            player.SetVelocityY(-playerData.wallSlideSpeed);
        }
        else
        {
            player.SetVelocityY(-playerData.wallSlideSpeed * 2);
        }

        if ((player.GrabToggled && player.AxisInput.y == 0) || player.AxisInput.y > 0)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if(player.AxisInput.y < 0 && player.IsGrounded)
        {
            player.InputHandler.ResetWallGrab();
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.audioManager.StopSFXLooped(slideAudioId);
    }
}
