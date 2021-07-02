using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "fistsData", menuName = "Data/Weapon/Fists")]
public class D_Weapon_Fists : ScriptableObject
{
    public float attackRadius;
    public AudioClip rightFistAttackSound;
    public AudioClip leftFistAttackSound;
    public AudioClip impactSound;
}
