using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditPeonKnockbackState : EnemyKnockbackState
{
    private Enemy_BanditPeon peon;
    public BanditPeonKnockbackState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditPeon peon) : base(enemy, stateMachine, stateData, animBoolName)
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

        if (Time.time < stateStartTime + (stateData.KnockbackTimer * stateData.KnockbackForce))
        {
            enemy.SetVelocityX(DecliningVelocity());
        }
        else if (peon.DetectPlayerMin())
        {
            stateMachine.ChangeState(peon.DetectedState);
        }
        else
        {
            stateMachine.ChangeState(peon.MoveState);
        }
    }
}
