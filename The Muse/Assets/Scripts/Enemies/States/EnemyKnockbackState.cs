using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : EnemyState
{
    public bool CanTakeDamage = true;

    private bool shouldFlip;

    private int direction;

    public EnemyKnockbackState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName) : base(enemy, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.damageDetails[1] < enemy.transform.position.x)
        {
            direction = 1;
            if (enemy.FacingDirection != -1)
            {
                shouldFlip = true;
            }
        }
        else
        {
            direction = -1;
            if (enemy.FacingDirection != 1)
            {
                shouldFlip = true;
            }
        }

        enemy.UpdateHealth(enemy.damageDetails[0]);
        enemy.SetVelocityZero();
        enemy.SetVelocityY(stateData.KnockbackForceY * stateData.KnockbackForce);

        CanTakeDamage = false;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.SetVelocityZero();
        enemy.tookDamage = false;
        CanTakeDamage = true;

        if (shouldFlip)
        {
            enemy.Flip();
            shouldFlip = false;
        }
    }

    public float DecliningVelocity()
    {
        float workspace;
        workspace = (stateData.KnockbackForceX * direction) * stateData.KnockbackForce;
        workspace -= Time.deltaTime;

        return workspace;
    }
}
