using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion
    #region Components
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public InputManager InputHandler { get; private set; }
    #endregion
    public int FacingDirection { get; private set; }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
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
        if (InputHandler.Input.x * FacingDirection == -1)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityX(float velocity)
    {
        Rb.velocity = new Vector2(velocity, Rb.velocity.y);
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
}
