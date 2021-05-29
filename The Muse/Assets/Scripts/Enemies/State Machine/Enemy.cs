using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enemyData StateData;
    public EnemyStateMachine StateMachine { get; private set; }

    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }

    protected Vector2 workspace;
    protected Vector2 CurrentVelocity;
    protected Vector2 hitBoxBotLeft;
    protected Vector2 hitBoxTopRight;

    public int currentHealth { get; private set; }

    private float[] attackDetails = new float[3];
    public float[] damageDetails = new float[2];

    public bool tookDamage;

    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected Transform ledgeCheck;
    [SerializeField]
    protected Transform hitBox;

    public int FacingDirection { get; set; }

    public virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        StateMachine = new EnemyStateMachine();
    }

    public virtual void Start()
    {
        FacingDirection = 1;

        currentHealth = StateData.MaxHealth;
    }

    public virtual void Update()
    {
        CurrentVelocity = RB.velocity;

        TouchDamageBox();

        StateMachine.currentState.LogicUpdate();
        Debug.Log(StateMachine.currentState);
    }

    public virtual void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    #region Set Functions
    public virtual void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = RB.velocity;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityZero()
    {
        workspace.Set(0, 0);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void UpdateHealth(float damageToTake)
    {
        currentHealth -= (int) damageToTake;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
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

    private void TouchDamageBox()
    {
        hitBoxBotLeft.Set(hitBox.position.x - (StateData.HitBoxWidth / 2), hitBox.position.y - (StateData.HitBoxHeight / 2));
        hitBoxTopRight.Set(hitBox.position.x + (StateData.HitBoxWidth / 2), hitBox.position.y + (StateData.HitBoxHeight / 2));

        Collider2D hit = Physics2D.OverlapArea(hitBoxBotLeft, hitBoxTopRight, StateData.whatIsPlayer);

        if (hit != null)
        {
            attackDetails[0] = StateData.TouchDamage;
            attackDetails[1] = transform.position.x;
            attackDetails[2] = StateData.KnockbackForce;
            hit.SendMessage("TakeDamage", attackDetails);
        }
    }

    public virtual void Damage(float[] attackDetails)
    {
        damageDetails[0] = attackDetails[0];
        damageDetails[1] = attackDetails[1];
        Debug.Log("hit enemy");
    }
    #endregion

    #region Other Functions
    public virtual void Flip()
    {
        FacingDirection *= -1;
        gameObject.transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawRay(ledgeCheck.position, Vector2.down * StateData.LedgeCheckDistance);
        Gizmos.DrawRay(wallCheck.position, gameObject.transform.right * StateData.WallCheckDistance);

        Vector2 botleft = new Vector2(hitBox.position.x - (StateData.HitBoxWidth / 2), hitBox.position.y - (StateData.HitBoxHeight / 2));
        Vector2 botRight = new Vector2(hitBox.position.x + (StateData.HitBoxWidth / 2), hitBox.position.y - (StateData.HitBoxHeight / 2));
        Vector2 topRight = new Vector2(hitBox.position.x + (StateData.HitBoxWidth / 2), hitBox.position.y + (StateData.HitBoxHeight / 2));
        Vector2 topLeft = new Vector2(hitBox.position.x - (StateData.HitBoxWidth / 2), hitBox.position.y + (StateData.HitBoxHeight / 2));

        Gizmos.DrawLine(botleft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botleft);
    }
    #endregion

}
