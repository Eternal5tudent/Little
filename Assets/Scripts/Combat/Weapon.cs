using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected D_Weapon weaponData;
    #region State Machine
    public WeaponStateMachine StateMachine { get; private set; }
    public WeaponIdleState IdleState { get; private set; }
    protected Action OnHitEnemy;
    protected Action OnAttack;
    #endregion

    #region Components 
    public Animator animator { get; private set; }
    #endregion

    public LayerMask whatIsEnemy { get; private set; }
    protected bool attackFinished = true;
    protected bool CooldownReady { get { return (Time.time - lastAttackTime >= weaponData.attackCooldown); } }
    private float lastAttackTime = 0;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        #region States Initialize
        StateMachine = new WeaponStateMachine();
        IdleState = new WeaponIdleState(this, StateMachine, weaponData, "idle");
        #endregion
    }

    protected virtual void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void TryAttack()
    {
        if (attackFinished && CooldownReady)
        {
            Attack();
        }
    }

    public abstract void Attack();

    public virtual void OnAnimationEnd()
    {
        StateMachine.CurrentState.OnAnimationEnd();
    }

    public void SetAttackTime()
    {
        lastAttackTime = Time.time;
        attackFinished = true;
    }

    public void SetEnemy(LayerMask whatIsEnemy)
    {
        this.whatIsEnemy = whatIsEnemy;
    }

    private void OnDisable()
    {
        OnHitEnemy = null;
    }

    /// <summary>
    /// What is going to happen when weapon detects enemy?
    /// </summary>
    /// <param name="action"></param>
    public void SetOnHit(Action action)
    {
        OnHitEnemy = null;
        OnHitEnemy += action;
    }

    /// <summary>
    /// Activates OnEnemyHit Action
    /// </summary>
    public void EnemyHit()
    {
        OnHitEnemy?.Invoke();
    }

    /// <summary>
    /// What is going to happen when weapon attacks?
    /// </summary>
    /// <param name="action"></param>
    public void SetOnAttack(Action action)
    {
        OnAttack = null;
        OnAttack += action;
    }

    /// <summary>
    /// Activates OnAttack Action
    /// </summary>
    public void ApplyAttackEffects()
    {
        OnAttack?.Invoke();
    }

}
