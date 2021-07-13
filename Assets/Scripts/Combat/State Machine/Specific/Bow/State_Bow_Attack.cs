using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Bow_Attack : WeaponState
{
    Weapon_Bow bow;
    public State_Bow_Attack(Weapon weapon, WeaponStateMachine stateMachine, D_Weapon weaponData, string animatorID, Weapon_Bow bow) : base(weapon, stateMachine, weaponData, animatorID)
    {
        this.bow = bow;
    }
    public override void Enter()
    {
        base.Enter();
        Attack();
    }

    public void Attack()
    {
        AudioManager.Instance.PlaySFX(weaponData.attackSound);
        bow.LaunchArrow();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void OnAnimationEnd()
    {
        base.OnAnimationEnd();
        weapon.SetAttackTime();
        stateMachine.ChangeState(bow.IdleState);
    }
}
