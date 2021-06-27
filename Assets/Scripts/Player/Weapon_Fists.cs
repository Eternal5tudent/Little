using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Fists : Weapon
{
    [SerializeField] Transform damageArea;
    [SerializeField] float damageRadius;


    protected override void Attack()
    {
        base.Attack();
        switch(attackNum)
        {
            case 0:
                animator.SetBool("attack1", true);
                break;                    
            case 1:                       
                animator.SetBool("attack2", true);
                break;
        }
    }

    protected override void OnAttackFinished()
    {
        base.OnAttackFinished();
        fighter.OnMeleeWeaponAttack();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(damageArea.transform.position, damageRadius, whatIsDamageable);
        foreach (Collider2D collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(weaponData.damage);
            }
        }
        switch(attackNum)
        {
            case 0:
                animator.SetBool("attack2", false);
                break;
            case 1:
                animator.SetBool("attack1", false);
                break;

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damageArea.position, damageRadius);
    }
}
