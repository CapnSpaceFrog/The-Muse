using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarMoveState : EnemyMoveState
{
    private Enemy_Boar boar;

    public BoarMoveState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_Boar boar) : base(enemy, stateMachine, stateData, animBoolName)
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

        if (boar.tookDamage)
        {
            stateMachine.ChangeState(boar.KnockbackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
