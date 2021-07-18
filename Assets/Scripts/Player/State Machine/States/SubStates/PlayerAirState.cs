using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    float dropForce;
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
        dropForce = playerData.dropForce;
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
        if(player.AxisInput.y < 0)
        {
            if(player.CurrentVelocity.y > -15f)
            {
                player.Rb.AddForce(Vector2.down * dropForce * Time.deltaTime, ForceMode2D.Impulse);
            }        
        }

        if (player.AxisInput.x * player.FacingDirection == -1)
        {
            player.Flip();
        }
        if (player.IsGrounded)
        {
            ChangeState(player.IdleState);
        }
        else if (player.IsTouchingWall)
        {
            ChangeState(player.WallSlideState);
        }
        else if (player.InputHandler.FireInput && !player.InputHandler.IsPointerOverUI)
        {
            ChangeState(player.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (player.CurrentVelocity.y < -30f)
        {
            player.SetVelocityY(-30f);
        }
    }
}
