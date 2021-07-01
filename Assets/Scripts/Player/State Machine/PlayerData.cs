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
    public float dropForce = 30f;
    public float wallJumpForce = 8;

    [Header("Surroundings")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;

    [Header("Combat")]
    public int maxHealth = 3;

    [Header("SFX")]
    public AudioClip footstep;
    public AudioClip hit_hurt;
    public AudioClip slide;
    public AudioClip jump;
}
