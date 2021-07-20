using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void TakeDamage(int damage, Vector2 attackOrigin);
    void Die();
}

public interface IFighter
{
    void OnMeleeWeaponAttack(bool hit);
}
