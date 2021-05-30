using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditPeonMoveState : EnemyMoveState
{
    private Enemy_BanditPeon peon;

    private bool hasNotAttemptedJump;
    private bool canFlip;

    private float lastJumpAttempt = -10;
    public BanditPeonMoveState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditPeon peon) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.peon = peon;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        enemy.SetVelocityX(stateData.MoveSpeed * enemy.FacingDirection);

        if (Time.time > lastJumpAttempt + stateData.JumpCooldown)
        {
            hasNotAttemptedJump = true;
        }

        if (peon.CheckIfGrounded())
        {
            canFlip = true;
            hasNotAttemptedJump = true;
        }
        else
        {
            canFlip = false;
        }

        if (!detectedLedge && canFlip)
        {
            enemy.Flip();
        }

        if (detectedWall && hasNotAttemptedJump)
        {
            peon.StateMachine.ChangeState(peon.JumpState);
            lastJumpAttempt = Time.time;
            hasNotAttemptedJump = false;
        }

        if (peon.DetectPlayerMin())
        {
            stateMachine.ChangeState(peon.DetectedState);
        }
        else if (peon.tookDamage)
        {
            stateMachine.ChangeState(peon.KnockbackState);
        }
    }
}
