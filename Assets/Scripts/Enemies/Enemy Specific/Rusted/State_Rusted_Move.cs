using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Rusted_Move : EnemyMoveState
{
    Enemy_Rusted rusted;
    public State_Rusted_Move(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Rusted rusted) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.rusted = rusted;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy.CheckWall() || !enemy.CheckGrounded())
        {
            rusted.IdleState.FlipAfterIdle(true);
            stateMachine.ChangeState(rusted.IdleState);
        }
        else if (enemy.CheckPlayer())
        {
            stateMachine.ChangeState(rusted.PlayerDetectedState);
        }
    }
}
