using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallState : PlayerState
{
    public PlayerWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGrounded && !player.GrabToggled)
        {
            stateMachine.ChangeState(player.IdleState);
            player.InputHandler.ResetWallGrab();
        }
        else if (!player.IsTouchingWall || player.AxisInput.x * player.FacingDirection == -1)
        {
            stateMachine.ChangeState(player.AirState);
            player.InputHandler.ResetWallGrab();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
