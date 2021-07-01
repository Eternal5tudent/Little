using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIdleState : WeaponState
{
    public WeaponIdleState(Weapon weapon, WeaponStateMachine stateMachine, D_Weapon weaponData, string animatorID) : base(weapon, stateMachine, weaponData, animatorID)
    {
    }
}
