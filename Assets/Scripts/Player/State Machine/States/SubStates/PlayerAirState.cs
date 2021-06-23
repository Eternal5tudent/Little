using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.ControlPlayer();
        if (player.AxisInput.x * player.FacingDirection == -1)
        {
            player.Flip();
        }
        if (player.IsGrounded)
            stateMachine.ChangeState(player.IdleState);
        else if (player.IsTouchingWall && player.InputHandler.JumpInput)
        {
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(player.IsTouchingWall && stateMachine.CurrentState!= player.JumpState)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
