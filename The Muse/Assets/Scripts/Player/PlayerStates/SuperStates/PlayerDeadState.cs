using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.gameObject.layer = 8;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityX(0);
        player.SetVelocityY(-9);

        if (player.CheckIfGrounded())
        {
            player.Anim.SetBool("dead", true);
            player.StartCoroutine("DeathText");
        }    
    }
}
