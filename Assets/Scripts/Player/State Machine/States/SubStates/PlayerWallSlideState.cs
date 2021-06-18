using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityY(-playerData.wallSlideSpeed);
        if ((player.GrabToggled && player.AxisInput.y == 0) || player.AxisInput.y > 0)
            stateMachine.ChangeState(player.WallGrabState);
        else if(player.AxisInput.y < 0 && player.IsGrounded)
        {
            player.InputHandler.ResetWallGrab();
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
