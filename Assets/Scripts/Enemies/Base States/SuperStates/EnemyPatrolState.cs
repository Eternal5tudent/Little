using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    protected bool facingWall;
    protected bool isGrounded;
    protected bool playerDetected;

    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        isGrounded = enemy.CheckGrounded();
        facingWall = enemy.CheckWall();
        playerDetected = enemy.CheckPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(playerDetected)
        {
            stateMachine.ChangeState(enemy.PlayerDetectedState);
        }
    }
}
