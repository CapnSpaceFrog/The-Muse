using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditPeonMoveState : EnemyMoveState
{
    protected Enemy_BanditPeon peon;
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
        
        if (peon.NeedsToJump())
        {
            stateMachine.ChangeState(peon.JumpState);
        }
        else if (peon.DetectPlayerMax())
        {
            stateMachine.ChangeState(peon.DetectedState);
        }
        else if (peon.DetectPlayerMin())
        {
            stateMachine.ChangeState(peon.AttackState);
        }
        else if (peon.tookDamage)
        {
            stateMachine.ChangeState(peon.KnockbackState);
        }
    }
}
