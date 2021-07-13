using System.Collections.Generic;
using UnityEngine;
public class Weapon_Bow : Weapon
{
    [SerializeField] Transform attackPos;

    public ObjectPool projectilePool { get; private set; }
    public State_Bow_Attack AttackState { get; private set; }
    private List<Projectile> arrows = new List<Projectile>();

    protected override void Awake()
    {
        base.Awake();
        projectilePool = GetComponentInChildren<ObjectPool>();
        AttackState = new State_Bow_Attack(this, StateMachine, weaponData, "attack", this);
    }

    protected override void Start()
    {
        base.Start();
        CreateArrows();
    }

    public override void Attack()
    {
        StateMachine.ChangeState(AttackState);
    }

    public void LaunchArrow()
    {
        Projectile arrow = GetArrow();
        arrow.transform.position = attackPos.position;
        arrow.transform.rotation = transform.rotation;
        arrow.gameObject.SetActive(true);
    }

    Projectile CreateNewArrow()
    {
        Projectile arrow = projectilePool.GetObject(false).GetComponent<Projectile>();
        arrows.Add(arrow);
        arrow.transform.parent = null;
        arrow.Initialize(whatIsEnemy, weaponData.damage);
        arrow.onHit += EnemyHit;
        return arrow;
    }

    private void CreateArrows()
    {
       
        for (int i = 0; i < 3; i++)
        {
            CreateNewArrow();
        }
    }

    private Projectile GetArrow()
    {
        foreach(Projectile arrow in arrows)
        {
            if(!arrow.gameObject.activeInHierarchy)
            {
                return arrow;
            }
        }

        return CreateNewArrow();
    }
}
