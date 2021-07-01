using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Data/Weapon")]
public class D_Weapon : ScriptableObject
{
    public float attackCooldown;
    public int damage = 1;
    public float idleAfterSecs = 1f;
}
