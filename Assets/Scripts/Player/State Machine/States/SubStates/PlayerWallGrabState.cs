using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerWallState
{
    private Vector2 enterPosition;
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enterPosition = player.transform.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGrounded && !player.GrabToggled)
        {
            ChangeState(player.IdleState);
            player.InputHandler.ResetWallGrab();
        }
        player.transform.position = enterPosition;
        if (!Transitioning)
        {
            player.SetVelocityX(0);
            player.SetVelocityY(0); 
        }
        if (player.AxisInput.y > 0)
        {
            ChangeState(player.WallClimbState);
        }
        else if(player.GrabToggled)
        {
            if (player.AxisInput.y < 0)
            {
                ChangeState(player.WallSlideState);
            }
        }
        else if(!player.GrabToggled)
        {
            ChangeState(player.WallSlideState);
        }
        
    }
}
