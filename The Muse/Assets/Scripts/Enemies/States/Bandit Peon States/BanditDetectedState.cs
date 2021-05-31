using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditDetectedState : EnemyMoveState
{
    private Enemy_BanditPeon peon;
    private float timeSinceLeftRange;
    private bool trigger;

    public BanditDetectedState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditPeon peon) : base(enemy, stateMachine, stateData, animBoolName)
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
        base.LogicUpdate();

        peon.SetVelocityX(stateData.ChargeSpeed * enemy.FacingDirection);

        if (peon.CheckIfGrounded())
        {
            peon.canFlip = true;
            peon.hasNotAttemptedJump = true;
        }
        else
        {
            peon.canFlip = false;
        }

        if (!detectedLedge && peon.canFlip)
        {
            enemy.Flip();
        }

        if (peon.DetectPlayerMin())
        {
            trigger = true;
        }

        if (peon.tookDamage)
        {
            stateMachine.ChangeState(peon.KnockbackState);
        }
        else if (peon.NeedsToJump())
        {
            stateMachine.ChangeState(peon.JumpState);
        }
        else if (peon.DetectPlayerMin())
        {
            stateMachine.ChangeState(peon.AttackState);
        }

        if (DetectionLeaveTimer())
        {
            stateMachine.ChangeState(peon.MoveState);
        }
    }

    private bool DetectionLeaveTimer()
    {
        if (!peon.DetectPlayerMax())
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
