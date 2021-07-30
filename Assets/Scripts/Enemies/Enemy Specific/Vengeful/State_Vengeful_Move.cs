using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Vengeful_Move : EnemyMoveState
{
    Enemy_Vengeful vengeful;
    public State_Vengeful_Move(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Vengeful vengeful) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.vengeful = vengeful;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (vengeful.CheckPlayer())
        {
            stateMachine.ChangeState(vengeful.PlayerDetectedState);
        }
        else if(vengeful.CheckWall() || !vengeful.CheckGrounded())
        {
            vengeful.IdleState.FlipAfterIdle(true);
            stateMachine.ChangeState(vengeful.IdleState);
        }
    }
}
