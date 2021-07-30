using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Solemn_Move : EnemyMoveState
{
    Enemy_Solemn solemn;
    public State_Solemn_Move(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Solemn solemn) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.solemn = solemn;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy.CheckWall() || !enemy.CheckGrounded())
        {
            solemn.IdleState.FlipAfterIdle(true);
            stateMachine.ChangeState(solemn.IdleState);
        }
        else if (enemy.CheckPlayer())
        {
            stateMachine.ChangeState(solemn.PlayerDetectedState);
        }
    }
}
