using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed = 10f;
    public float jumpPower = 15;

    [Header("Check surroundings")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
}
