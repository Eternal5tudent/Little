using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTalkState : PlayerState
{
    public PlayerTalkState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        DialogueBox.OnDialogueBoxClosed += StopInteracting;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);
        player.InteractWithNPC();
    }

    public void StopInteracting()
    {
        stateMachine.ChangeState(player.IdleState);
    }
}
