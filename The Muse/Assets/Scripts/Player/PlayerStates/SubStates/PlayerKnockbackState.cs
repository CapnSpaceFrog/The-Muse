using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : PlayerState
{
    public bool CanTakeDamage = true;

    private int direction;

    public PlayerKnockbackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player.damageDetails[1] < player.transform.position.x)
        {
            direction = 1;
        } else
        {
            direction = -1;
        }

        player.SetVelocityZero();
        player.SetVelocityY(playerData.knockbackVelocityY * player.damageDetails[2]);

        CanTakeDamage = false;
    }

    public override void Exit()
    {
        base.Exit();

        player.tookDamage = false;
        CanTakeDamage = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time < startTime + (playerData.knockbackTimer * player.damageDetails[2]))
        {
            player.SetVelocityX(DecliningVelocity());
        }
        else
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public float DecliningVelocity()
    {
        float workspace;
        workspace = (playerData.knockbackVelocityX * direction * player.damageDetails[2]);
        workspace -= Time.deltaTime;

        return workspace;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
