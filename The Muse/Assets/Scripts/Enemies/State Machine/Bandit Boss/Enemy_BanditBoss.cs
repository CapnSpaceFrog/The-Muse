using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BanditBoss : Enemy
{
    public BanditBossMoveState MoveState { get; private set; }
    public BanditBossDetectState DetectedState { get; private set; }
    public BanditBossKnockbackState KnockbackState { get; private set; }
    public BanditBossJumpState JumpState { get; private set; }
    public BanditBossAttackState AttackState { get; private set; }
    public BanditBossThrowState ThrowState { get; private set; }

    [SerializeField]
    protected Transform playerCheck;
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected Transform attackCheck;
    [SerializeField]
    protected Transform jumpHighCheck;
    [SerializeField]
    protected Transform jumpLowCheck;
    [SerializeField]
    protected Transform projectileSpawnPos;

    [SerializeField]
    private GameObject bossKnifePrefab;

    [HideInInspector]
    public bool attackFinished;
    [HideInInspector]
    public bool hasNotAttemptedJump = false;
    [HideInInspector]
    public bool canFlip;

    private float threwKnifeTime = -10;

    public override void Awake()
    {
        base.Awake();

        MoveState = new BanditBossMoveState(this, StateMachine, StateData, "move", this);
        DetectedState = new BanditBossDetectState(this, StateMachine, StateData, "move", this);
        KnockbackState = new BanditBossKnockbackState(this, StateMachine, StateData, "knockback", this);
        JumpState = new BanditBossJumpState(this, StateMachine, StateData, "jump", this);
        AttackState = new BanditBossAttackState(this, StateMachine, StateData, "attack", this);
        ThrowState = new BanditBossThrowState(this, StateMachine, StateData, "throw", this);
    }

    public override void Start()
    {
        base.Start();

        StateMachine.Initialize(MoveState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void Damage(float[] attackDetails)
    {
        base.Damage(attackDetails);

        if (KnockbackState.CanTakeDamage)
        {
            tookDamage = true;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawRay(playerCheck.position, gameObject.transform.right * StateData.PlayerCheckDistanceMin);
        Gizmos.DrawRay(playerCheck.position, gameObject.transform.right * StateData.PlayerCheckDistanceMax);

        Gizmos.DrawWireSphere(groundCheck.transform.position, StateData.GroundCheckRadius);
        Gizmos.DrawWireSphere(attackCheck.transform.position, StateData.AttackCheckRadius);

        Gizmos.DrawWireSphere(jumpHighCheck.position, StateData.JumpCheckRadius);
        Gizmos.DrawWireSphere(jumpLowCheck.position, StateData.JumpCheckRadius);
    }

    #region Check Functions
    public bool DetectPlayerMax()
    {
        return Physics2D.Raycast(playerCheck.position, gameObject.transform.right, StateData.PlayerCheckDistanceMax, StateData.whatIsPlayer);
    }

    public bool DetectPlayerMin()
    {
        return Physics2D.Raycast(playerCheck.position, gameObject.transform.right, StateData.PlayerCheckDistanceMin, StateData.whatIsPlayer);
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, StateData.GroundCheckRadius, StateData.whatIsGround);
    }

    public bool CheckAttackHitbox()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackCheck.position, StateData.AttackCheckRadius, StateData.whatIsPlayer);

        if (hit != null)
        {
            attackDetails[0] = StateData.TouchDamage;
            attackDetails[1] = transform.position.x;
            attackDetails[2] = StateData.KnockbackForce;
            hit.SendMessage("TakeDamage", attackDetails);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FinishAttack()
    {
        attackFinished = true;
    }

    public bool NeedsToJump()
    {
        if (!CheckLowJump())
        {
            StateData.JumpVelocity = StateData.JumpVelocityLow;
        }
        else if (!CheckHighJump())
        {
            StateData.JumpVelocity = StateData.JumpVelocityHigh;
        }

        if (CheckLowJump() && CheckHighJump())
        {
            Flip();
        }

        if (CheckWall() && hasNotAttemptedJump && (!CheckLowJump() || !CheckHighJump()))
        {
            hasNotAttemptedJump = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckHighJump()
    {
        return Physics2D.OverlapCircle(jumpHighCheck.position, StateData.JumpCheckRadius, StateData.whatIsGround);
    }

    private bool CheckLowJump()
    {
        return Physics2D.OverlapCircle(jumpLowCheck.position, StateData.JumpCheckRadius, StateData.whatIsGround);
    }

    public void InstantiateProjectile()
    {
            bossKnifePrefab.GetComponent<BanditProjectile>().direction = FacingDirection;
            Instantiate(bossKnifePrefab, projectileSpawnPos.position, projectileSpawnPos.rotation);
            threwKnifeTime = Time.time;
    }

    public bool CanThrowKnife()
    {
        if (Time.time > threwKnifeTime + StateData.ThrowKnifeCooldown)
        {
            return true;
        }
        return false;
    }
    #endregion
}
