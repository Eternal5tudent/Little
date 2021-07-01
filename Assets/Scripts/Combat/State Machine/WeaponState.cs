using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState 
{
    protected Weapon weapon;
    protected D_Weapon weaponData;
    protected string animatorID;
    protected WeaponStateMachine stateMachine;
    protected float enterTime;

    public WeaponState(Weapon weapon, WeaponStateMachine stateMachine, D_Weapon weaponData, string animatorID)
    {
        this.weapon = weapon;
        this.weaponData = weaponData;
        this.animatorID = animatorID;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        enterTime = Time.time;
        weapon.animator.SetBool(animatorID, true);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {
        weapon.animator.SetBool(animatorID, false);
    }

    public virtual void OnAnimationEnd()
    {

    }

}
