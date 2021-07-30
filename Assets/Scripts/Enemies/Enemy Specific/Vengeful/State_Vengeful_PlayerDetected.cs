using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Vengeful_PlayerDetected : EnemyState
{
    Enemy_Vengeful vengeful;
    public State_Vengeful_PlayerDetected(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Vengeful vengeful) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.vengeful = vengeful;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (TimePassed >= enemyData.playerDetectedAwaitTime)
        {
            stateMachine.ChangeState(vengeful.AttackState);
        }
    }
}
