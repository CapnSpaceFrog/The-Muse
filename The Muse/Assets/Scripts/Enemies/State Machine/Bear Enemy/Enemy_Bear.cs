using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bear : Enemy
{
    public EnemyPlayerDetectedState DetectedState { get; private set; }
    public BearMoveState BearMoveState { get; private set; }

    [SerializeField]
    private Transform playerCheck;

    public override void Awake()
    {
        base.Awake();

        BearMoveState = new BearMoveState(this, StateMachine, StateData, "move", this);
        DetectedState = new EnemyPlayerDetectedState(this, StateMachine, StateData, "detected", this);
    }

    public override void Start()
    {
        base.Start();

        StateMachine.Initialize(BearMoveState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(playerCheck.position, gameObject.transform.right * StateData.PlayerCheckDistanceMin);
        Gizmos.DrawRay(playerCheck.position, gameObject.transform.right * StateData.PlayerCheckDistanceMax);
        Gizmos.DrawRay(ledgeCheck.position, Vector2.down * StateData.LedgeCheckDistance);
        Gizmos.DrawRay(wallCheck.position, gameObject.transform.right * StateData.WallCheckDistance);
    }
}
