using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayMoveDust(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(input.x == 0)
        {
            ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.ControlPlayer();
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayMoveDust(false);
    }

}
