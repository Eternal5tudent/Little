using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rusted : Enemy
{
    public State_Rusted_Idle IdleState { get; private set; }
    public State_Rusted_Move MoveState { get; private set; }
    public State_Rusted_PlayerDetected PlayerDetectedState { get; private set; }
    public State_Rusted_Charge ChargeState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new State_Rusted_Idle(this, StateMachine, enemyData, "idle", this);
        MoveState = new State_Rusted_Move(this, StateMachine, enemyData, "move", this);
        PlayerDetectedState = new State_Rusted_PlayerDetected(this, StateMachine, enemyData, "playerDetected", this);
        ChargeState = new State_Rusted_Charge(this, StateMachine, enemyData, "charge", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }
}
