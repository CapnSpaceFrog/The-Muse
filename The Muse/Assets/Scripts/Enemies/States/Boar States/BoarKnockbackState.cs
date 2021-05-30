using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarKnockbackState : EnemyKnockbackState
{
    private Enemy_Boar boar;

    public BoarKnockbackState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_Boar boar) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.boar = boar;
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

        if (Time.time < stateStartTime + (stateData.KnockbackTimer * stateData.KnockbackForce))
        {
            enemy.SetVelocityX(DecliningVelocity());
        }
        else
        {
            stateMachine.ChangeState(boar.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
