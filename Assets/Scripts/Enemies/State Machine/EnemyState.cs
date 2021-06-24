using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{

    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected D_Enemy enemyData;
    private readonly string animBoolName;
    protected float startTime;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, D_Enemy enemyData, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        enemy.Anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationStartedTrigger()
    {

    }
    public virtual void AnimationFinishedTrigger()
    {

    }

}

