using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void PhysicsUpdate()
    {
        base.LogicUpdate();
        enemy.SetVelocityX(enemyData.movementSpeed * enemy.FacingDirection);
    }
}
