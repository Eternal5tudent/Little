using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    float timer = 0;
    float duration = 0.1f;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        player.Flip();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetVelocity(new Vector2(player.FacingDirection * 7, 14));
        timer += Time.fixedDeltaTime;
        if (timer >= duration)
        {
            player.ConserveSpeed(true);
            stateMachine.ChangeState(player.AirState);
        }
    }
}
