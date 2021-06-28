using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerWallState
{
    bool detectingLedge = true;
    public bool enteredGrounded = false;
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        detectingLedge = player.CheckLedge();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGrounded && !player.GrabToggled && !enteredGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
            player.InputHandler.ResetWallGrab();
        }
        player.SetVelocityY(playerData.wallClimbSpeed);
        if(player.AxisInput.y <= 0)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if(!detectingLedge && player.IsTouchingWall)
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        detectingLedge = player.CheckLedge();
    }

    public override void Exit()
    {
        base.Exit();
        enteredGrounded = false;
    }
}
