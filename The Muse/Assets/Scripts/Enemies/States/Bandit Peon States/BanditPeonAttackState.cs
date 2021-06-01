using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditPeonAttackState : EnemyState
{
    private Enemy_BanditPeon peon;
    public BanditPeonAttackState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditPeon peon) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.peon = peon;
    }
    public override void Enter()
    {
        base.Enter();

        peon.SetVelocityZero();
        peon.attackFinished = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (peon.attackFinished && peon.DetectPlayerMin())
        {
            stateMachine.ChangeState(peon.AttackState);
        }
        else if (peon.attackFinished && !peon.DetectPlayerMin() && peon.DetectPlayerMax())
        {
            stateMachine.ChangeState(peon.DetectedState);
        }
        else if (!peon.DetectPlayerMin() && !peon.DetectPlayerMax())
        {
            stateMachine.ChangeState(peon.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
