using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Rusted_Charge : EnemyState
{
    Enemy_Rusted rusted;
    float chargeTime = 0.3f;
    public State_Rusted_Charge(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Rusted rusted) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.rusted = rusted;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.SetVelocityX(enemyData.movementSpeed * 2 * enemy.FacingDirection);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (TimePassed >= chargeTime)
        {
            rusted.IdleState.FlipAfterIdle(false);
            stateMachine.ChangeState(rusted.IdleState);
        }
    }
}
