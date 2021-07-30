using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected float idleTime = 1;
    bool flipAfterIdle = true;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
        if(flipAfterIdle)
            enemy.Flip();
    }

    public void FlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }
}
