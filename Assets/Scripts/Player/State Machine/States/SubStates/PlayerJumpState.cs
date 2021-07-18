using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
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
        canHold = Time.time < startTime + playerData.jumpHoldTime;
        if (canHold)
        {
            if (player.InputHandler.JumpHold)
            {
                player.SetVelocityY(playerData.jumpPower);
            }
            
        }
        else
        {
            player.SetVelocityY(playerData.jumpPower / 1.5f);
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
        if(!player.InputHandler.JumpHold && !Transitioning)
        {
            player.SetVelocityY(0);
            isAbilityDone = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
