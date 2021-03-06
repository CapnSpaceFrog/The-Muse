using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    public PlayerKnockbackState KnockbackState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerWonState WonState { get; private set; }


    public PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerHealthVisuals HealthSystem { get; private set; }
    public LevelLoader transition;
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    #endregion

    #region Other Variables
    public float[] damageDetails = new float[3];
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    public float lastSpellCast = -10;

    private Vector2 workspace;

    [SerializeField]
    private GameObject spellIcon;

    public bool tookDamage { get; set; }
    public bool hasWon;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        HealthSystem = GameObject.Find("HealthVisual").GetComponent<PlayerHealthVisuals>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        KnockbackState = new PlayerKnockbackState(this, StateMachine, playerData, "knockback");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", 1);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", 2);
        DeadState = new PlayerDeadState(this, StateMachine, playerData, "");
        WonState = new PlayerWonState(this, StateMachine, playerData, "");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        Inventory = GetComponent<PlayerInventory>();

        FacingDirection = 1;

        PrimaryAttackState.SetWeapon(Inventory.weapon[(int)CombatInputs.primary]);
        SecondaryAttackState.SetWeapon(Inventory.weapon[(int)CombatInputs.secondary]);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();

        if (CheckIfCanSpellCast()) {
            spellIcon.SetActive(true);
        } else {
            spellIcon.SetActive(false);
        }

        if (hasWon)
        {
            SetVelocityZero();
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
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
    #endregion

    #region Check Functions
    public bool CheckIfCanSpellCast()
    {
        if (lastSpellCast + playerData.SpellCooldown > Time.time)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool CheckIfGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround))
        {
            return true;
        }
        else if (Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsBox))
        {
            return true;
        }
        else return false;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection && !InputHandler.PausedGame)
        {
            Flip();
        }
    }

    public void TakeDamage(float[] attackDetails)
    {
        damageDetails[0] = attackDetails[0];
        damageDetails[1] = attackDetails[1];
        damageDetails[2] = attackDetails[2];

        if (KnockbackState.CanTakeDamage)
        {
            tookDamage = true;
        }
    }
    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, playerData.groundCheckRadius);
    }

    public void GameOver()
    {
        Physics2D.IgnoreLayerCollision(7, 3, true);
    }

    IEnumerator DeathText()
    {
        yield return new WaitForSeconds(1f);
        transition.GameOver();
    }

    public void GameWon()
    {
        StateMachine.ChangeState(WonState);
    }

    public IEnumerator DelayedWin()
    {
        yield return new WaitForSeconds(2f);
        transition.GameWon();
    }
    #endregion
}
