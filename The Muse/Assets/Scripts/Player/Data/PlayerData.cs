using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    public int MaxHealth;
    public float LyreDamage;
    public float SpellDamage;
    public float SpellCooldown;

    [Header("Move State")]
    public float movementVelocity = 10;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Knockback State")]
    public float knockbackVelocityY = 10f;
    public float knockbackVelocityX = 5f;
    public float knockbackTimer;

    [Header("Attack State")]
    public int test;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public LayerMask whatIsBox;
}
