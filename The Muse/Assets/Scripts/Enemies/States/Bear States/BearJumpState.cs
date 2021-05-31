using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearJumpState : EnemyState
{
    private Enemy_Bear bear;

    public bool IsJumping { get; private set; }
    public BearJumpState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_Bear bear) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.bear = bear;
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

        if (bear.CheckIfGrounded() && bear.CurrentVelocity.y < 0.01f)
        {
            IsJumping = false;
        }
    }

    private void CheckJumpMultiplier()
    {
        if (IsJumping && !bear.CheckIfGrounded())
        {
            enemy.SetVelocityY(enemy.CurrentVelocity.y * stateData.JumpHeightMultiplier);
            IsJumping = false;
            stateMachine.ChangeState(bear.MoveState);
        }
    }
}
