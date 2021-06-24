using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy")]
public class D_Enemy : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed = 8f;
    public float idleTime = 1f;
    [Header("Sorroundings")]
    public float groundCheckRay;
    public float wallCheckRay;
    public LayerMask whatIsGround;
}
