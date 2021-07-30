using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Rusted_PlayerDetected : EnemyState
{
    Enemy_Rusted rusted;
    public State_Rusted_PlayerDetected(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Rusted rusted) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.rusted = rusted;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.LogicUpdate();
        enemy.SetVelocityX(0);
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        stateMachine.ChangeState(rusted.ChargeState);
    }

}
