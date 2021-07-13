using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.PlaySound_Jump();
        player.SetVelocityY(playerData.jumpPower);
        isAbilityDone = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.ControlPlayer();
    }
}
