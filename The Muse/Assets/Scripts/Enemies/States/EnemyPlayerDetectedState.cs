using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyState
{
    protected Enemy_Bear bear;

    private bool detectedPlayerMin;
    
    public EnemyPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_Bear bear) : base(enemy, stateMachine, stateData, animBoolName)
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

        bear.SetVelocityX(stateData.ChargeSpeed * enemy.FacingDirection);

        if (!bear.DetectPlayerMax())
        {
            stateMachine.ChangeState(bear.BearMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
