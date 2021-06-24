using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform groundedCheck;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform ledgeCheck;

    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    #endregion

    #region Components
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public InputManager InputHandler { get; private set; }
    #endregion

    #region Condition variables
    public int FacingDirection { get; private set; }
    public Vector2 AxisInput { get { return InputHandler.AxisInput; } }
    public bool GrabToggled { get { return InputHandler.GrabWallToggle; } }
    public Vector2 CurrentVelocity { get { return Rb.velocity; } }
    public bool IsGrounded { get; private set; }
    public bool IsTouchingWall { get; private set; }
    private bool speedIsConserved = false;
    #endregion

    #region Unity
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        AirState = new PlayerAirState(this, StateMachine, playerData, "air");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "air");
    }

    private void Start()
    {
        InputHandler = InputManager.Instance;
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        FacingDirection = 1;
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        if (GrabToggled && !IsTouchingWall)
            InputHandler.ResetWallGrab();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        DoChecks();
    }

    public void DoChecks()
    {
        IsGrounded = Physics2D.OverlapCircle(groundedCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
        IsTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, playerData.wallCheckDistance, playerData.whatIsGround);
    }
    #endregion

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundedCheck.position, playerData.groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * playerData.wallCheckDistance);
    }
    public void ControlPlayer()
    { 
        if(speedIsConserved)
        {
            if (AxisInput.x != 0)
                ConserveSpeed(false);
        }
        if (!IsTouchingWall && !speedIsConserved) 
            SetVelocityX(playerData.movementSpeed * AxisInput.x); 
    }

    public virtual void AnimationStartedTrigger()
    {
        StateMachine.CurrentState.AnimationStartedTrigger();
    }
    public virtual void AnimationFinishedTrigger()
    {
        StateMachine.CurrentState.AnimationFinishedTrigger();
    }

    public void ConserveSpeed(bool conserve)
    {
        speedIsConserved = conserve;
    }

    public bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, transform.right, playerData.wallCheckDistance, playerData.whatIsGround);
    }
}
