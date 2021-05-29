using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bear : Enemy
{
    public BearDetectedPlayerState DetectedState { get; private set; }
    public BearMoveState MoveState { get; private set; }
    public BearKnockbackState KnockbackState { get; private set; }

    [SerializeField]
    private Transform playerCheck;

    public override void Awake()
    {
        base.Awake();

        MoveState = new BearMoveState(this, StateMachine, StateData, "move", this);
        DetectedState = new BearDetectedPlayerState(this, StateMachine, StateData, "detected", this);
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
            Debug.Log(tookDamage);
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
    #endregion

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawRay(playerCheck.position, gameObject.transform.right * StateData.PlayerCheckDistanceMin);
        Gizmos.DrawRay(playerCheck.position, gameObject.transform.right * StateData.PlayerCheckDistanceMax);
    }
}
