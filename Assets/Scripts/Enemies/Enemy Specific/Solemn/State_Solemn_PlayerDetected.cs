using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Solemn_PlayerDetected : EnemyState
{
    Enemy_Solemn solemn;
    public State_Solemn_PlayerDetected(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Solemn solemn) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.solemn = solemn;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.SetVelocityX(0);
        if (TimePassed >= enemyData.playerDetectedAwaitTime)
        {
            stateMachine.ChangeState(solemn.AttackState);
        }
    }
}
