using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;
    protected bool jumpInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        input = player.InputHandler.AxisInput;
        jumpInput = player.InputHandler.JumpInput;
        if (player.AxisInput.x * player.FacingDirection == -1)
        {
            player.Flip();
        }
        if (jumpInput)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (player.IsTouchingWall && player.GrabToggled)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (player.IsTouchingWall && player.InputHandler.AxisInput.y > 0)
        {
            player.WallClimbState.enteredGrounded = true;
            stateMachine.ChangeState(player.WallClimbState);
        }
        else if (!player.IsGrounded)
        {
            stateMachine.ChangeState(player.AirState);
        }     
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
