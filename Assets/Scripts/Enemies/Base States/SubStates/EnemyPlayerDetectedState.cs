using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyState
{
    float attackCountdown;
    bool playerDetected;
    public EnemyPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        attackCountdown = enemyData.playerDetectedAwaitTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.SetVelocityX(0);
        if(Time.time - startTime >= attackCountdown)
        {
            if (playerDetected)
                stateMachine.ChangeState(enemy.AttackState);
            else
                stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        playerDetected = enemy.CheckPlayer();
    }
}
