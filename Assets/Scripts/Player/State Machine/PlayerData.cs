using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed = 10f;
    public float jumpPower = 15;
    public float wallSlideSpeed = 3f;
    public float wallClimbSpeed = 2f;

    [Header("Surroundings")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
}
