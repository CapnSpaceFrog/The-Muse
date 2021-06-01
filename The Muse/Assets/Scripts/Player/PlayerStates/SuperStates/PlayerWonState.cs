using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWonState : PlayerState
{
    public PlayerWonState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityZero();

        if (player.hasWon)
        {
            player.Anim.SetBool("won", true);
            player.StartCoroutine("DelayedWin");
        }
    }
}
