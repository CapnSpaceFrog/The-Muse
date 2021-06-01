using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boar : Enemy
{
    public BoarMoveState MoveState { get; private set; }
    public BoarKnockbackState KnockbackState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        MoveState = new BoarMoveState(this, StateMachine, StateData, "move", this);
        KnockbackState = new BoarKnockbackState(this, StateMachine, StateData, "move", this);
    }

    public override void Start()
    {
        base.Start();

        StateMachine.Initialize(MoveState);
    }

    public override void Update()
    {
        base.Update();

        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Damage(float[] attackDetails)
    {
        base.Damage(attackDetails);

        if (KnockbackState.CanTakeDamage)
        {
            tookDamage = true;
        }
    }
}
