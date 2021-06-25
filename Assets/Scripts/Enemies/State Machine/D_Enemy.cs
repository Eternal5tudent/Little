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
    public float wallCheckRay;
    public float playerCheckRay;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    [Header("Combat")]
    public float playerDetectedAwaitTime = 0.8f;
    public int maxHealth;
}
