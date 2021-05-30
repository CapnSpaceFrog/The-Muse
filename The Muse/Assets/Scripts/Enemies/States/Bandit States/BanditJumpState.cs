using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditJumpState : EnemyState
{
    private Enemy_BanditPeon peon;

    public bool isJumping { get; private set; }
    public BanditJumpState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditPeon peon) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.peon = peon;
    }

    public override void Enter()
    {
        base.Enter();

        isJumping = true;
        peon.SetVelocityY(stateData.JumpVelocity);
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
            isJumping = false;
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping && !peon.CheckIfGrounded())
        {
            peon.SetVelocityY(peon.CurrentVelocity.y * stateData.JumpHeightMultiplier);
            isJumping = false;
            stateMachine.ChangeState(peon.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
