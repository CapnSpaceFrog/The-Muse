using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBossAttackState : EnemyState
{
    private Enemy_BanditBoss boss;
    public BanditBossAttackState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName, Enemy_BanditBoss boss) : base(enemy, stateMachine, stateData, animBoolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();

        boss.SetVelocityZero();
        boss.attackFinished = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (boss.tookDamage)
        {
            stateMachine.ChangeState(boss.KnockbackState);
        }
        else if (boss.attackFinished && boss.DetectPlayerMin())
        {
            stateMachine.ChangeState(boss.AttackState);
        }
        else if (boss.attackFinished && !boss.DetectPlayerMin() && boss.DetectPlayerMax())
        {
            if (boss.CanThrowKnife())
            {
                stateMachine.ChangeState(boss.ThrowState);
            } else if (boss.attackFinished)
            {
                stateMachine.ChangeState(boss.DetectedState);
            }
        }
    }
}
