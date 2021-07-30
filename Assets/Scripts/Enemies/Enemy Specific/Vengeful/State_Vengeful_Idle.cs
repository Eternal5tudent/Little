using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Vengeful_Idle : EnemyIdleState
{
    Enemy_Vengeful vengeful;
    public State_Vengeful_Idle(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Vengeful vengeful) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.vengeful = vengeful;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        vengeful.SetVelocityX(0);
        if (vengeful.CheckPlayer())
        {
            stateMachine.ChangeState(vengeful.PlayerDetectedState);
        }
        else if (TimePassed >= idleTime)
        {
            stateMachine.ChangeState(vengeful.MoveState);
        }
    }
}
