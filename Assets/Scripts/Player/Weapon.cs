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
    bool hidden = false;
    public virtual void Initialize(LayerMask whatIsDamageable)
    {
        this.whatIsDamageable = whatIsDamageable;
    }

    protected virtual void Start()
    {
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
        //spriteRenderer.enabled = true;       
    }

    private void Update()
    {
        if(Time.time - lastTimeAttacked >= weaponData.resetAfterTime)
        {
            if(!hidden)
                Hide();
        }
    }

    public virtual void Hide()
    {
        attackNum = 0;
        //spriteRenderer.enabled = false;
        animator.SetTrigger("hide");
        hidden = true;
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
        hidden = false;
    }
}
