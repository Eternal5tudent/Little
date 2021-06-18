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
        if (player.IsGrounded)
            stateMachine.ChangeState(player.IdleState);
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
