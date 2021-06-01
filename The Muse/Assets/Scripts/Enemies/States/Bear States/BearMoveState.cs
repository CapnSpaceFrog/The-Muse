using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMoveState : EnemyMoveState
{
    private Enemy_Bear bear;

    public BearMoveState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_Bear bear) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.bear = bear;
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

        if (bear.CheckIfGrounded())
        {
            bear.canFlip = true;
            bear.hasNotAttemptedJump = true;
        }
        else
        {
            bear.canFlip = false;
        }

        if (!detectedLedge && bear.canFlip)
        {
            enemy.Flip();
        }

        if (bear.NeedsToJump())
        {
            stateMachine.ChangeState(bear.JumpState);
        }
        else if (bear.DetectPlayerMin())
        {
            stateMachine.ChangeState(bear.DetectedState);
        }
        else if (bear.tookDamage)
        {
            stateMachine.ChangeState(bear.KnockbackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
