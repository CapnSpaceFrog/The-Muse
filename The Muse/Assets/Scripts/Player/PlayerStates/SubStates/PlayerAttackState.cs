using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private int typeOfAttack;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, int typeOfAttack) : base(player, stateMachine, playerData, animBoolName)
    {
        this.typeOfAttack = typeOfAttack;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityZero();
    }

    public override void Enter()
    {
        base.Enter();

        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        if (typeOfAttack == 2)
        {
            player.lastSpellCast = Time.time;
        }
        weapon.ExitWeapon();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
    }

    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
    #endregion
}
