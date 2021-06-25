using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbillityState : EnemyState
{
    protected bool abilityDone;
    public EnemyAbillityState(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        abilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(abilityDone)
        {
            //todo: transition to idle state and set "shouldFlip" to false
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        abilityDone = false;
    }
}
