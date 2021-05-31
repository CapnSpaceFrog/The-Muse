using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class enemyData : ScriptableObject
{
    [Header("Stats")]
    public int MaxHealth;

    [Header("Jump State")]
    public float JumpVelocity;
    public float JumpVelocityHigh;
    public float JumpVelocityLow;
    public float JumpHeightMultiplier;
    public float GroundCheckRadius;
    public float JumpCheckRadius;

    [Header("Attack State")]
    public float AttackCheckRadius;

    [Header("Throw State")]
    public int ProjectileDamage;
    public float ProjectileSpeed;
    public float ThrowKnifeCooldown;

    [Header("Detect State")]
    public float DeagroTimeLimit;

    [Header("Touch Box Collider")]
    public float HitBoxHeight;
    public float HitBoxWidth;
    public int TouchDamage;

    [Header("Knockback State")]
    public float KnockbackForce;
    public float KnockbackTimer;
    public float KnockbackForceX;
    public float KnockbackForceY;

    [Header("Move State")]
    public float MoveSpeed = 3f;
    public float ChargeSpeed = 6f;

    [Header("Check Variables")]
    public float WallCheckDistance;
    public float LedgeCheckDistance;
    public float PlayerCheckDistanceMin;
    public float PlayerCheckDistanceMax;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
