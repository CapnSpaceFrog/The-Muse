using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enemyData StateData;
    public EnemyStateMachine StateMachine;

    protected EnemyMoveState MoveState;

    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }

    private Vector2 workspace;
    private Vector2 CurrentVelocity;

    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected Transform ledgeCheck;

    public int FacingDirection { get; set; }



    public virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        StateMachine = new EnemyStateMachine();
        MoveState = new EnemyMoveState(this, StateMachine, StateData, "move");
    }

    public virtual void Start()
    {
        FacingDirection = 1;

        StateMachine.Initialize(MoveState);
    }

    public virtual void Update()
    {
        CurrentVelocity = RB.velocity;

        StateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    #region Set Functions
    public virtual void SetVelocityX(float velocityToSet)
    {
        workspace.Set(velocityToSet, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = RB.velocity;
    }
    #endregion

    #region Check Functions
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, gameObject.transform.right, StateData.WallCheckDistance, StateData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, StateData.LedgeCheckDistance, StateData.whatIsGround);
    }
    #endregion

    #region Other Functions
    public virtual void Flip()
    {
        FacingDirection *= -1;
        gameObject.transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ledgeCheck.position, Vector2.down * StateData.LedgeCheckDistance);
        Gizmos.DrawRay(wallCheck.position, gameObject.transform.right * StateData.WallCheckDistance);
    }
    #endregion

}
