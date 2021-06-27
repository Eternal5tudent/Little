using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void TakeDamage(int damage);
    void Die();
}

public interface IFighter
{
    void OnWeaponAttackFinished();
}
