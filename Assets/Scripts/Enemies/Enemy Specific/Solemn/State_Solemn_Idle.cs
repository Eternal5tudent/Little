using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Solemn_Idle : EnemyIdleState
{
    Enemy_Solemn solemn;
    public State_Solemn_Idle(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Solemn solemn) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.solemn = solemn;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(TimePassed >= idleTime)
        {
            stateMachine.ChangeState(solemn.MoveState);
        }
        else if (enemy.CheckPlayer())
        {
            stateMachine.ChangeState(solemn.PlayerDetectedState);
        }
    }
}
