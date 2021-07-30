using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected D_Enemy enemyData;
    [SerializeField] protected Transform groundedCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected Material hitMaterial;

    private Material originalMat;

    #region States
    public EnemyStateMachine StateMachine { get; protected set; }
    #endregion

    #region Condition variables
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get { return Rb.velocity; } }
    public int CurrentHealth { get; protected set; }
    public int MaxHealth { get; protected set; }
    public bool IsControllable { get; protected set; } = true;
    #endregion

    #region Components
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    #endregion

    #region Unity
    protected virtual void Awake()
    {
        StateMachine = new EnemyStateMachine();
    }

    protected virtual void Start()
    {

        MaxHealth = enemyData.maxHealth;
        CurrentHealth = MaxHealth;
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMat = spriteRenderer.material;
        FacingDirection = 1;
    }

    protected virtual void Update()
    {
        if(StateMachine.CurrentState != null)
            StateMachine.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        if(StateMachine.CurrentState != null)
            StateMachine.CurrentState.PhysicsUpdate();
    }

    protected virtual void OnDrawGizmos()
    {
        DrawSurroundingsChecks();
        Gizmos.DrawWireSphere(transform.position, enemyData.attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(playerCheck.position, Vector3.right * enemyData.playerCheckRay);
    }

    protected void DrawSurroundingsChecks()
    {
        Gizmos.DrawRay(groundedCheck.position, transform.up * -1 * enemyData.groundCheckRay);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(wallCheck.position, enemyData.wallCheckBox);
        Gizmos.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable playerDamageable = collision.GetComponent<IDamageable>();
            if (playerDamageable != null)
            {
                print("damage");
                playerDamageable.TakeDamage(1, transform.position);
                AttackKnockback(transform.position - collision.transform.position, 0.1f);
            }
        }
    }

    #endregion

    #region Control

    public void SetVelocityX(float velocity, bool skipControl = false)
    {
        if (IsControllable || skipControl)
            Rb.velocity = new Vector2(velocity, CurrentVelocity.y);
    }

    public void SetVelocityY(float velocity, bool skipControl = false)
    {
        if (IsControllable || skipControl)
            Rb.velocity = new Vector2(CurrentVelocity.x, velocity);
    }

    public void SetVelocity(Vector2 velocity, bool skipControl = false)
    {
        if (IsControllable || skipControl)
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
        return Physics2D.BoxCast(wallCheck.position, enemyData.wallCheckBox, 0f, Vector2.right, 0, enemyData.whatIsGround);
    }

    public virtual bool CheckPlayer()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, enemyData.playerCheckRay, enemyData.whatIsPlayer);
    }
    #endregion

    public virtual void TakeDamage(int damage, Vector2 attackOrigin)
    {
        CurrentHealth--;
        EnableDamageShader();
        GenerateHitParticles();
        PlayHitSound();
        AttackKnockback((Vector2)transform.position - attackOrigin, 0.07f);
        if(!CheckPlayer())
        {
            Flip();
        }
        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        PlayDeathSound();
        GenerateHitParticles();
        Destroy(gameObject);
    }

    public void EnableDamageShader()
    {
        StartCoroutine(EnableDamageShader_Cor());
    }

    IEnumerator EnableDamageShader_Cor()
    {
        spriteRenderer.material = hitMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = originalMat;
    }

    public void AttackKnockback(Vector2 direction, float seconds)
    {
        direction = direction.normalized;
        IEnumerator KnockBack_Cor()
        {
            IsControllable = false;
            float startTime = Time.time;
            while (Time.time < startTime + seconds)
            {
                SetVelocity(direction * 10, true);
                yield return new WaitForEndOfFrame();
            }
            SetVelocity(Vector2.zero, true);
            IsControllable = true;
        }
        StartCoroutine(KnockBack_Cor());
    }

    public void PlayDeathSound()
    {
        AudioManager.Instance.PlaySFX(enemyData.deathSound);
    }

    public void PlayHitSound()
    {
        AudioManager.Instance.PlaySFX(enemyData.hitSound);
    }

    public void GenerateHitParticles()
    {
        Instantiate(enemyData.hitParticles.gameObject, transform.position, Quaternion.identity, null);
    }

}
