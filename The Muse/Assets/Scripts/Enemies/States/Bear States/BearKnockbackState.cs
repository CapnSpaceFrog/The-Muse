using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearKnockbackState : EnemyKnockbackState
{
    private Enemy_Bear bear;
    public BearKnockbackState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_Bear bear) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.bear = bear;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time < stateStartTime + stateData.KnockbackTimer)
        {
            enemy.SetVelocityX(DecliningVelocity());
        }
        else if (bear.DetectPlayerMin())
        {
            stateMachine.ChangeState(bear.DetectedState);
        }
        else
        {
            stateMachine.ChangeState(bear.MoveState);
        }
    }
}
