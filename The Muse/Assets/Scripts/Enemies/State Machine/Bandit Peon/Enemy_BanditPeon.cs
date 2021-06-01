using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BanditPeon : Enemy
{
    public BanditPeonMoveState MoveState { get; private set; }
    public BanditDetectedState DetectedState { get; private set; }
    public BanditPeonKnockbackState KnockbackState { get; private set; }
    public BanditJumpState JumpState { get; private set; }
    public BanditPeonAttackState AttackState { get; private set; }

    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform attackCheck;
    [SerializeField]
    protected Transform jumpHighCheck;
    [SerializeField]
    protected Transform jumpLowCheck;

    [HideInInspector]
    public bool attackFinished;
    [HideInInspector]
    public bool hasNotAttemptedJump = false;
    [HideInInspector]
    public bool canFlip;

    public override void Awake()
    {
        base.Awake();

        MoveState = new BanditPeonMoveState(this, StateMachine, StateData, "move", this);
        KnockbackState = new BanditPeonKnockbackState(this, StateMachine, StateData, "knockback", this);
        DetectedState = new BanditDetectedState(this, StateMachine, StateData, "move", this);
        JumpState = new BanditJumpState(this, StateMachine, StateData, "jump", this);
        AttackState = new BanditPeonAttackState(this, StateMachine, StateData, "attack", this);
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
            Debug.Log("took damage");
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
            base.attackDetails[0] = StateData.TouchDamage;
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
    #endregion
}
