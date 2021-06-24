using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyPatrolState
{
    float idleTime;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        idleTime = enemyData.idleTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time - startTime >= idleTime)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Flip();
    }
}
