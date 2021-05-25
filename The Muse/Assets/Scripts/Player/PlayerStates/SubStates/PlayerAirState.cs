using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirSState : PlayerState
{
    public PlayerAirSState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
}
