using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Player player;
    private PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    private readonly string animBoolName;
    protected float startTime;
    protected bool Transitioning { get; private set; }

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        Transitioning = false;
        player.Anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
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

    public virtual void ChangeState(PlayerState newState)
    {
        if (!Transitioning)
        {
            stateMachine.ChangeState(newState);
            Transitioning = true;
        }
    }
}
