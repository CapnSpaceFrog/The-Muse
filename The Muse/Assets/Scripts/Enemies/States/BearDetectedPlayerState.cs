using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDetectedPlayerState : EnemyState
{
    protected Enemy_Bear bear;

    private bool detectedPlayerMin;
    
    public BearDetectedPlayerState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_Bear bear) : base(enemy, stateMachine, stateData, animBoolName)
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

        if (bear.tookDamage)
        {
            stateMachine.ChangeState(bear.KnockbackState);
        }
        else if (!bear.DetectPlayerMax())
        {
            stateMachine.ChangeState(bear.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
