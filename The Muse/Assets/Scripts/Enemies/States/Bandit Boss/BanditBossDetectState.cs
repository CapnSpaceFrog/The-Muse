using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBossDetectState : EnemyMoveState
{
    private Enemy_BanditBoss boss;
    private float timeSinceLeftRange;
    private bool trigger;

    public BanditBossDetectState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditBoss boss) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.boss = boss;
    }
    public override void Enter()
    {
        base.Enter();

        trigger = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        boss.SetVelocityX(stateData.ChargeSpeed * enemy.FacingDirection);

        if (boss.CheckIfGrounded())
        {
            boss.canFlip = true;
            boss.hasNotAttemptedJump = true;
        }
        else
        {
            boss.canFlip = false;
        }

        if (!detectedLedge && boss.canFlip)
        {
            enemy.Flip();
        }

        if (boss.DetectPlayerMax())
        {
            trigger = true;
        }

        if (boss.tookDamage)
        {
            stateMachine.ChangeState(boss.KnockbackState);
        }
        else if (boss.NeedsToJump())
        {
            stateMachine.ChangeState(boss.JumpState);
        }
        else if (boss.DetectPlayerMax() && boss.CanThrowKnife())
        {
            stateMachine.ChangeState(boss.ThrowState);
        }
        else if (boss.DetectPlayerMin())
        {
            stateMachine.ChangeState(boss.AttackState);
        }

        if (DetectionLeaveTimer())
        {
            stateMachine.ChangeState(boss.MoveState);
        }
    }

    private bool DetectionLeaveTimer()
    {
        if (!boss.DetectPlayerMax())
        {
            if (trigger)
            {
                timeSinceLeftRange = Time.time;
                trigger = false;
            }

            if (Time.time > timeSinceLeftRange + stateData.DeagroTimeLimit)
            {
                return true;
            }
        }
        return false;
    }
}
