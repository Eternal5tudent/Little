using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Fists_Wait : WeaponState
{
    Weapon_Fists fists;
    private int lastAttack;
    public State_Fists_Wait(Weapon weapon, D_Weapon weaponData, WeaponStateMachine stateMachine, string animatorID, Weapon_Fists fists) : base(weapon, stateMachine, weaponData, animatorID)
    {
        this.fists = fists;
    }

    public void SetLastAttack(int lastAttack)
    {
        this.lastAttack = lastAttack;
    }

    public void Attack()
    {
        switch(lastAttack)
        {
            case 1:
                stateMachine.ChangeState(fists.attack2State);
                break;
            case 2:
                stateMachine.ChangeState(fists.attack1State);
                break;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time - enterTime >= weaponData.idleAfterSecs)
        {
            stateMachine.ChangeState(weapon.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        lastAttack = 1;
    }

}
