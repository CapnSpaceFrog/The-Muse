using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBossKnockbackState : EnemyKnockbackState
{
    private Enemy_BanditBoss boss;
    public BanditBossKnockbackState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditBoss boss) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.boss = boss;
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
        else if (boss.DetectPlayerMax())
        {
            stateMachine.ChangeState(boss.DetectedState);
        }
        else
        {
            stateMachine.ChangeState(boss.MoveState);
        }
    }
}
