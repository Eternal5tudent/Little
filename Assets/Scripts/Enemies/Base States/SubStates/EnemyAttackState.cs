using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyAbillityState
{
    bool detectingPlayer;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemyData.attackRadius, enemyData.whatIsDamageable);
        foreach (Collider2D collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(enemyData.damage, enemy.transform.position);
            }
        }
        if (detectingPlayer)
            stateMachine.ChangeState(enemy.PlayerDetectedState);
        else
            abilityDone = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        detectingPlayer = enemy.CheckPlayer();
    }
}
