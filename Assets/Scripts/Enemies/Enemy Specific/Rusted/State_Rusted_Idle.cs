using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Rusted_Idle : EnemyIdleState
{
    Enemy_Rusted rusted;
    public State_Rusted_Idle(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Rusted rusted) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.rusted = rusted;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (TimePassed >= idleTime)
        {
            stateMachine.ChangeState(rusted.MoveState);
        }
        else if(enemy.CheckPlayer())
        {
            stateMachine.ChangeState(rusted.PlayerDetectedState);
        }
        Debug.Log("idling");

    }
}
