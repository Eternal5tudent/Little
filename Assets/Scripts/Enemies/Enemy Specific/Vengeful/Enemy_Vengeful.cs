using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vengeful : Enemy
{
    [SerializeField] Projectile projectile;
    [SerializeField] Transform firePos;
    public State_Vengeful_Idle IdleState { get; private set; }
    public State_Vengeful_Move MoveState { get; private set; }
    public State_Vengeful_PlayerDetected PlayerDetectedState { get; private set; }
    public State_Vengeful_Attack AttackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new State_Vengeful_Idle(this, StateMachine, enemyData, "idle", this);
        MoveState = new State_Vengeful_Move(this, StateMachine, enemyData, "move", this);
        PlayerDetectedState = new State_Vengeful_PlayerDetected(this, StateMachine, enemyData, "playerDetected", this);
        AttackState = new State_Vengeful_Attack(this, StateMachine, enemyData, "attack", this, projectile, firePos);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    public override bool CheckPlayer()
    {
        return Physics2D.OverlapBox(playerCheck.position, new Vector2(enemyData.playerCheckRay, 1), 0, enemyData.whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        DrawSurroundingsChecks();
        // Draw attack box
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerCheck.position, new Vector2(enemyData.playerCheckRay, 1));
    }
}
