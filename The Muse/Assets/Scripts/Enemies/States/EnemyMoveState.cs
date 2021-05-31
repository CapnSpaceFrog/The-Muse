using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public bool detectedWall { get; private set; }
    public bool detectedLedge { get; private set; }

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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        detectedLedge = enemy.CheckLedge();
        detectedWall = enemy.CheckWall();
    }
}
