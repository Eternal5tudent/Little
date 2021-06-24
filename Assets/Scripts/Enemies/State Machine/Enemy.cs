using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] D_Enemy enemyData;
    [SerializeField] Transform groundedCheck;
    [SerializeField] Transform wallCheck;

    #region States
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    #endregion

    #region Components
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    #endregion

    #region Condition variables
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get { return Rb.velocity; } }
    #endregion

    #region Unity
    protected virtual void Awake()
    {
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, enemyData, "move");
    }

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        FacingDirection = 1;
        StateMachine.Initialize(MoveState);
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawRay(groundedCheck.position, transform.up  * -1 * enemyData.groundCheckRay);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * enemyData.wallCheckRay);
    }

    #endregion

    #region Control

    public void SetVelocityX(float velocity)
    {
        Rb.velocity = new Vector2(velocity, CurrentVelocity.y);
    }

    public void SetVelocityY(float velocity)
    {
        Rb.velocity = new Vector2(CurrentVelocity.x, velocity);
    }

    public void SetVelocity(Vector2 velocity)
    {
        Rb.velocity = velocity;
    }

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public virtual void AnimationStartedTrigger()
    {
        StateMachine.CurrentState.AnimationStartedTrigger();
    }

    public virtual void AnimationFinishedTrigger()
    {
        StateMachine.CurrentState.AnimationFinishedTrigger();
    }
    #endregion

    #region Physics Checks
    public bool CheckGrounded()
    {
        return Physics2D.Raycast(groundedCheck.position, transform.up * -1, enemyData.groundCheckRay, enemyData.whatIsGround);
    }

    public bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, enemyData.wallCheckRay, enemyData.whatIsGround);
    }
    #endregion

}
