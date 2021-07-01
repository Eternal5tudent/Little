using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Fists : Weapon
{
    [SerializeField] D_MeleeWeapon meleeData;
    [SerializeField] Transform attackPos;
    public State_Fists_Attack1 attack1State { get; private set; }
    public State_Fists_Attack2 attack2State { get; private set; }
    public State_Fists_Wait waitState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        attack1State = new State_Fists_Attack1(this, weaponData, StateMachine, "attack1", attackPos, this, meleeData);
        attack2State = new State_Fists_Attack2(this, weaponData, StateMachine, "attack2", attackPos, this, meleeData);
        waitState = new State_Fists_Wait(this, weaponData, StateMachine, "idle", this);
    }

    public override void Attack()
    {
        if (StateMachine.CurrentState == IdleState)
        {
            StateMachine.ChangeState(attack1State);
        }
        else if (StateMachine.CurrentState == waitState)
        {
            waitState.Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, meleeData.attackRadius);
    }
}
