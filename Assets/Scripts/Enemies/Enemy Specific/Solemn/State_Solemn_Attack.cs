using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Solemn_Attack : EnemyState
{
    Enemy_Solemn solemn;

    public State_Solemn_Attack(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Solemn solemn) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.solemn = solemn;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.SetVelocityX(0);
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        Collider2D hit = Physics2D.OverlapCircle(enemy.transform.position, enemyData.attackRadius, enemyData.whatIsPlayer);
        if (hit != null)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1, enemy.transform.position);
                enemy.AttackKnockback(enemy.transform.position - hit.transform.position, 0.1f);
            }
            solemn.IdleState.FlipAfterIdle(false);
        }
        stateMachine.ChangeState(solemn.IdleState);
    }
}
