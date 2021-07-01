using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Fists_Attack1 : WeaponState
{
    Weapon_Fists fists;
    D_MeleeWeapon data;
    Transform attackPos;

    public State_Fists_Attack1(Weapon weapon, D_Weapon weaponData, WeaponStateMachine stateMachine, string animatorID, Transform attackPos, Weapon_Fists fists, D_MeleeWeapon data) : base(weapon, stateMachine, weaponData, animatorID)
    {
        this.fists = fists;
        this.data = data;
        this.attackPos = attackPos;
    }

    public override void OnAnimationEnd()
    {
        base.OnAnimationEnd();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, data.attackRadius, weapon.whatIsEnemy); 
        foreach (Collider2D collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(weaponData.damage);
                weapon.OnHitEnemy?.Invoke();
            }
        }
        weapon.SetAttackTime();
        fists.waitState.SetLastAttack(1);
        stateMachine.ChangeState(fists.waitState);
    }
}
