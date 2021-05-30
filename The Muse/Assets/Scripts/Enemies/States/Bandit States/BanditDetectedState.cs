using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditDetectedState : EnemyState
{
    private Enemy_BanditPeon peon;
    public BanditDetectedState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditPeon peon) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.peon = peon;
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

        peon.SetVelocityX(stateData.ChargeSpeed * enemy.FacingDirection);

        if (peon.tookDamage)
        {
            stateMachine.ChangeState(peon.KnockbackState);
        }
        else if (!peon.DetectPlayerMax())
        {
            stateMachine.ChangeState(peon.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
