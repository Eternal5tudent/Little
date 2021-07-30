using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Solemn : Enemy
{
    public State_Solemn_Idle IdleState { get; private set; }
    public State_Solemn_Move MoveState { get; private set; }
    public State_Solemn_Attack AttackState { get; private set; }
    public State_Solemn_PlayerDetected PlayerDetectedState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new State_Solemn_Idle(this, StateMachine, enemyData, "idle", this);
        MoveState = new State_Solemn_Move(this, StateMachine, enemyData, "move", this);
        AttackState = new State_Solemn_Attack(this, StateMachine, enemyData, "attack", this);
        PlayerDetectedState = new State_Solemn_PlayerDetected(this, StateMachine, enemyData, "playerDetected", this);
    }


    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }
}
