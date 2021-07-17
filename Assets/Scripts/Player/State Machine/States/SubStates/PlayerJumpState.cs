using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    const float maxHoldTime = 0.25f;
    bool canHold;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.PlaySound_Jump();
        player.SetVelocityY(playerData.jumpPower);
        player.InputHandler.ConsumeJump();
        canHold = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.ControlPlayer();
        canHold = Time.time < startTime + maxHoldTime;
        if (canHold)
        {
            if (player.InputHandler.JumpHold)
            {
                player.SetVelocityY(playerData.jumpPower);
            }
            else
            {
                isAbilityDone = true;
            }
        }
        else
        {
            isAbilityDone = true;
        }
        if (player.CurrentVelocity.y <= 0)
        {
            isAbilityDone = true;
        }
        if (player.InputHandler.FireInput && !player.InputHandler.IsPointerOverUI)
        {
            ChangeState(player.AttackState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
