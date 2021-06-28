using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Data/Weapon")]
public class D_Weapon : ScriptableObject
{
    public int chainedAttacks = 1;
    public float resetAfterTime = 1;
    public int damage = 1;
}
