using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : EnemyState
{
    public bool CanTakeDamage;

    public EnemyKnockbackState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName) : base(enemy, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityZero();
        enemy.SetVelocityY(stateData.KnockbackForceY * stateData.KnockbackForce);

        CanTakeDamage = false;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.tookDamage = false;
        CanTakeDamage = true;
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
            stateMachine.ChangeState(enemy.MoveState);
        }
    }
    public float DecliningVelocity()
    {
        float workspace;
        workspace = (stateData.KnockbackForceX * enemy.FacingDirection) * stateData.KnockbackForce;
        workspace -= Time.deltaTime;

        return workspace;
    }
}
