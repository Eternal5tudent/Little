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
        player.SetVelocityY(playerData.jumpPower); 
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.ControlPlayer();
        Debug.Log(player.CurrentVelocity.y);
        if (player.CurrentVelocity.y <= 7)
            isAbilityDone = true;
    }
}
