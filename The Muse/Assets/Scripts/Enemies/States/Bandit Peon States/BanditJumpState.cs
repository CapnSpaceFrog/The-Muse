using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditJumpState : EnemyState
{
    private Enemy_BanditPeon peon;

    public bool IsJumping { get; private set; }
    public BanditJumpState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditPeon peon) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.peon = peon;
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

        if (peon.CheckIfGrounded() && peon.CurrentVelocity.y < 0.01f)
        {
            IsJumping = false;
        }
    }

    private void CheckJumpMultiplier()
    {
        if (IsJumping && !peon.CheckIfGrounded())
        {
            enemy.SetVelocityY(enemy.CurrentVelocity.y * stateData.JumpHeightMultiplier);
            IsJumping = false;
            stateMachine.ChangeState(peon.MoveState);
        }
    }
}
