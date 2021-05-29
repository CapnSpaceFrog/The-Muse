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

        if (bear.DetectPlayerMin())
        {
            stateMachine.ChangeState(bear.DetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
