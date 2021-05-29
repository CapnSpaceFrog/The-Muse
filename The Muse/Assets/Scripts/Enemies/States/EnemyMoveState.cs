using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected bool detectedWall;
    protected bool detectedLedge;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName) : base(enemy, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        detectedLedge = enemy.CheckLedge();
        detectedWall = enemy.CheckWall();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetVelocityX(stateData.MoveSpeed * enemy.FacingDirection);

        if (detectedWall || !detectedLedge)
        {
            enemy.Flip();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        detectedLedge = enemy.CheckLedge();
        detectedWall = enemy.CheckWall();
    }
}
