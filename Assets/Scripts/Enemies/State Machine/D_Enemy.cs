using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy")]
public class D_Enemy : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed = 8f;
    public float idleTime = 1f;
    [Header("Surroundings")]
    public float groundCheckRay;
    public Vector2 wallCheckBox;
    public float playerCheckRay;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    [Header("Combat")]
    public float playerDetectedAwaitTime = 0.8f;
    public int maxHealth;
    public float attackRadius;
    public LayerMask whatIsDamageable;
    public int damage;
    public ParticleSystem hitParticles;
    [Header("Sounds")]
    public AudioClip deathSound;
    public AudioClip hitSound;
}
