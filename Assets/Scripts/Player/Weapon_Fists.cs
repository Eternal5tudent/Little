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
                animator.SetTrigger("attack1");
                break;
            case 1:
                animator.SetTrigger("attack2");
                break;
        }
        //print(attackNum);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(damageArea.transform.position, damageRadius, whatIsDamageable);
        foreach (Collider2D collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(weaponData.damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damageArea.position, damageRadius);
    }
}
