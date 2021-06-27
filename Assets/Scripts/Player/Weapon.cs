using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected D_Weapon weaponData;
    protected Animator animator;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    protected int attackNum = 0;
    protected LayerMask whatIsDamageable;
    bool canAttack = true;
    float lastTimeAttacked;
    bool idling = false;
    protected IFighter fighter;
    public virtual void Initialize(LayerMask whatIsDamageable, IFighter fighter)
    {
        this.whatIsDamageable = whatIsDamageable;
        this.fighter = fighter;
    }

    protected virtual void Start()
    {
        lastTimeAttacked = Time.time;  
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    public void TryAttack()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        canAttack = false;
        if(idling)
            animator.SetBool("idle", false);
    }

    private void Update()
    {
        if(Time.time - lastTimeAttacked >= weaponData.resetAfterTime)
        {
            if(!idling)
                Idle();
        }
    }

    public virtual void Idle()
    {
        attackNum = 0;
        //spriteRenderer.enabled = false;
        animator.SetBool("idle", true);
        idling = true;
        canAttack = true;
    }

    protected virtual void OnAttackFinished()
    {
        attackNum++;
        //print(attackNum);
        if (attackNum >= weaponData.chainedAttacks)
            attackNum = 0;
        canAttack = true;
        lastTimeAttacked = Time.time;
        idling = false;
    }
}
