using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Vengeful_Attack : EnemyState
{
    Enemy_Vengeful vengeful;
    Projectile projectile;
    Transform firePos;

    public State_Vengeful_Attack(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName, Enemy_Vengeful vengeful, Projectile projectile, Transform firePos) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.vengeful = vengeful;
        this.projectile = projectile;
        this.firePos = firePos;
    }

    public override void Enter()
    {
        base.Enter();
        
        Projectile proj = Object.Instantiate(projectile.gameObject, firePos.position, vengeful.transform.rotation).GetComponent<Projectile>();
        proj.Initialize(enemyData.whatIsPlayer, enemyData.damage);
        proj.SetDestroyAfterwards(true);
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        vengeful.IdleState.FlipAfterIdle(false);
        stateMachine.ChangeState(vengeful.IdleState);
    }

}
