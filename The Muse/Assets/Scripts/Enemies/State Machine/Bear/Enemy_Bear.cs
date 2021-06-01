using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bear : Enemy
{
    public BearDetectedPlayerState DetectedState { get; private set; }
    public BearMoveState MoveState { get; private set; }
    public BearKnockbackState KnockbackState { get; private set; }
    public BearJumpState JumpState { get; private set; }

    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform jumpCheck;

    [HideInInspector]
    public bool hasNotAttemptedJump = false;
    [HideInInspector]
    public bool canFlip;

    public override void Awake()
    {
        base.Awake();

        MoveState = new BearMoveState(this, StateMachine, StateData, "move", this);
        DetectedState = new BearDetectedPlayerState(this, StateMachine, StateData, "detected", this);
        JumpState = new BearJumpState(this, StateMachine, StateData, "jump", this);
        KnockbackState = new BearKnockbackState(this, StateMachine, StateData, "knockback", this);
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

    public bool NeedsToJump()
    {
        if (JumpCheck())
        {
            Flip();
        }

        if (CheckWall() && hasNotAttemptedJump && !JumpCheck())
        {
            hasNotAttemptedJump = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool JumpCheck()
    {
        return Physics2D.OverlapCircle(jumpCheck.position, StateData.JumpCheckRadius, StateData.whatIsGround);
    }
    #endregion

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawRay(playerCheck.position, gameObject.transform.right * StateData.PlayerCheckDistanceMin);
        Gizmos.DrawRay(playerCheck.position, gameObject.transform.right * StateData.PlayerCheckDistanceMax);

        Gizmos.DrawWireSphere(groundCheck.position, StateData.GroundCheckRadius);
        Gizmos.DrawWireSphere(jumpCheck.position, StateData.JumpCheckRadius);
    }
}
