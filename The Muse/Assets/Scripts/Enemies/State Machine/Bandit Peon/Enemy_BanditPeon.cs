using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BanditPeon : Enemy
{
    public BanditPeonMoveState MoveState { get; private set; }
    public BanditDetectedState DetectedState { get; private set; }
    public BanditPeonKnockbackState KnockbackState { get; private set; }
    public BanditJumpState JumpState { get; private set; }

    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;

    public override void Awake()
    {
        base.Awake();

        MoveState = new BanditPeonMoveState(this, StateMachine, StateData, "move", this);
        KnockbackState = new BanditPeonKnockbackState(this, StateMachine, StateData, "knockback", this);
        DetectedState = new BanditDetectedState(this, StateMachine, StateData, "detected", this);
        JumpState = new BanditJumpState(this, StateMachine, StateData, "jump", this);
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
    #endregion
}
