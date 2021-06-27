using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    Weapon currentWeapon;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        //currentWeapon = player.CurrentWeapon;
        //currentWeapon.TryAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate(); 
    }
}
