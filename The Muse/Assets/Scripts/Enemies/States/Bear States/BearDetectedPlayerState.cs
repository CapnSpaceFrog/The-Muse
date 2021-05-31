using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDetectedPlayerState : EnemyMoveState
{
    protected Enemy_Bear bear;
    private float timeSinceLeftRange;
    private bool trigger;

    public BearDetectedPlayerState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_Bear bear) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.bear = bear;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        bear.SetVelocityX(stateData.ChargeSpeed * enemy.FacingDirection);

        if (bear.CheckIfGrounded())
        {
            bear.canFlip = true;
            bear.hasNotAttemptedJump = true;
        }
        else
        {
            bear.canFlip = false;
        }

        //if (!detectedLedge && bear.canFlip)
        //{
        //    enemy.Flip();
        //}

        if (bear.DetectPlayerMin())
        {
            trigger = true;
        }

        if (bear.NeedsToJump())
        {
            stateMachine.ChangeState(bear.JumpState);
        }
        else if (bear.tookDamage)
        {
            stateMachine.ChangeState(bear.KnockbackState);
        }
        else if (!bear.DetectPlayerMax())
        {
            stateMachine.ChangeState(bear.MoveState);
        }

        if (DetectionLeaveTimer())
        {
            stateMachine.ChangeState(bear.MoveState);
        }
    }

    private bool DetectionLeaveTimer()
    {
        if (!bear.DetectPlayerMax())
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
