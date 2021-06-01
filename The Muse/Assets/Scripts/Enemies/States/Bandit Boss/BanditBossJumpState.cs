using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBossJumpState : EnemyState
{
    private Enemy_BanditBoss boss;

    public bool IsJumping { get; private set; }
    public BanditBossJumpState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditBoss boss) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();

        IsJumping = true;
        enemy.SetVelocityZero();
        enemy.SetVelocityX(stateData.MoveSpeed);
        enemy.SetVelocityY(stateData.JumpVelocity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckJumpMultiplier();

        if (boss.CheckIfGrounded() && enemy.CurrentVelocity.y < 0.01f)
        {
            IsJumping = false;
        }
    }

    private void CheckJumpMultiplier()
    {
        if (IsJumping && !boss.CheckIfGrounded())
        {
            enemy.SetVelocityY(enemy.CurrentVelocity.y * stateData.JumpHeightMultiplier);
            IsJumping = false;
            if (boss.DetectPlayerMax())
            {
                stateMachine.ChangeState(boss.DetectedState);
            }
            else
            {
                stateMachine.ChangeState(boss.MoveState);
            }
        }
    }
}
