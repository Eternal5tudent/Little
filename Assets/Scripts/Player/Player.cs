using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dialogue_Udemy;

public class Player : Singleton<Player>, IDamageable, IFighter
{
    [SerializeField] Transform weaponPos;
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform groundedCheck;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform ledgeCheck;
    [SerializeField] UnityEvent OnDamage;
    [SerializeField] UnityEvent OnDeath;   

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
    public PlayerAttackState AttackState { get; private set; }
    public PlayerTalkState TalkState { get; private set; }
    #endregion

    #region Components
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public InputManager InputHandler { get; private set; }
    public AudioManager audioManager { get; private set; }
    #endregion

    #region Variables
    private Material originalMat;
    private Material flashMat;
    public int FacingDirection { get; private set; }
    public Vector2 AxisInput { get { return InputHandler.AxisInput; } }
    public bool GrabToggled { get { return InputHandler.GrabWallToggle; } }
    public Vector2 CurrentVelocity { get { return Rb.velocity; } }
    public bool IsGrounded { get; private set; }
    public bool IsTouchingWall { get; private set; }
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    private bool speedIsConserved = false;
    public Weapon CurrentWeapon { get; private set; }
    public NPC nearbyNPC { get; private set; }

    #endregion

    #region Unity
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        AirState = new PlayerAirState(this, StateMachine, playerData, "air");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "air");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "idle");
        TalkState = new PlayerTalkState(this, StateMachine, playerData, "idle");
    }

    private void Start()
    {
        MaxHealth = playerData.maxHealth;
        CurrentHealth = MaxHealth;
        InputHandler = InputManager.Instance;
        Rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMat = spriteRenderer.material;
        flashMat = EffectManager.Instance.flashMaterial;
        Anim = GetComponent<Animator>();
        FacingDirection = 1;
        StateMachine.Initialize(IdleState);
        audioManager = AudioManager.Instance;
        EquipWeapon(playerData.StartingWeapon);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        if (GrabToggled && !IsTouchingWall)
        { 
            InputHandler.ResetWallGrab(); 
        }
           
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        DoChecks();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("NPC"))
        {
            NPC npc = collision.GetComponent<NPC>();
            if (npc != null)
            {
                SetNearbyNPC(npc, true);
                Debug.Log("NPC nearby");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            NPC npc = collision.GetComponent<NPC>();
            if (npc != null)
            {
                SetNearbyNPC(npc, false);
                Debug.Log("No NPC nearby");

            }
        }
    }
    #endregion

    #region Character Control
    public void InteractWithNPC()
    {
        if(nearbyNPC != null)
        {
            nearbyNPC.Interact();
        }
    }

    private void SetNearbyNPC(NPC npc, bool isNearby)
    {
        if(isNearby)
        {
            nearbyNPC = npc;
        }
        else
        {
            nearbyNPC = null;
        }
    }

    public void DoChecks()
    {
        IsGrounded = Physics2D.OverlapCircle(groundedCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
        IsTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, playerData.wallCheckDistance, playerData.whatIsGround);
    }

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

    public void TakeDamage(int damage)
    {
        CurrentHealth--;
        if (CurrentHealth <= 0)
            Die();
        OnDamage?.Invoke();
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        newWeapon = Instantiate(newWeapon.gameObject, weaponPos.position, transform.rotation, transform).GetComponent<Weapon>();
        if(CurrentWeapon!=null)
        {
            audioManager.PlaySFX(playerData.equipWeapon);
            Destroy(CurrentWeapon.gameObject);
        }
        CurrentWeapon = newWeapon;
        CurrentWeapon.SetEnemy(playerData.whatIsEnemy);
        CurrentWeapon.SetOnHit(() => { CameraManager.Instance.Shake(1, 0.2f); });
    }
    #endregion

    #region Effects
    public void EnableHitMaterial()
    {
        if(gameObject.activeInHierarchy)
            StartCoroutine(EnableHitMaterial_Cor());
    }

    private IEnumerator EnableHitMaterial_Cor()
    {
        spriteRenderer.material = flashMat;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = originalMat;

    }

    public void OnMeleeWeaponAttack(bool hit)
    {
        IEnumerator OnMeleeWeaponAttack_Cor()
        {
            if (hit)
                Time.timeScale = 0.05f;
            yield return new WaitForSecondsRealtime(0.1f);
            Time.timeScale = 1;
            SetVelocityX(FacingDirection * 4f);
            yield return new WaitForSeconds(0.05f);
            SetVelocityX(0f);
        }
       
        StartCoroutine(OnMeleeWeaponAttack_Cor());
    }

    public void PlaySound_Footsteps()
    {
        audioManager.PlaySFX(playerData.footstep);
    }

    public void PlaySound_Hurt()
    {
        audioManager.PlaySFX(playerData.hit_hurt);
    }

    public void PlaySound_Jump()
    {
        audioManager.PlaySFX(playerData.jump);
    }
    #endregion
}