using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBossMoveState : EnemyMoveState
{
    protected Enemy_BanditBoss boss;
    public BanditBossMoveState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditBoss boss) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.boss = boss;
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

        if (boss.NeedsToJump())
        {
            stateMachine.ChangeState(boss.JumpState);
        }
        else if (boss.DetectPlayerMax())
        {
            stateMachine.ChangeState(boss.DetectedState);
        }
        else if (boss.DetectPlayerMin())
        {
            stateMachine.ChangeState(boss.AttackState);
        }
        else if (boss.tookDamage)
        {
            stateMachine.ChangeState(boss.KnockbackState);
        }
    }


}
