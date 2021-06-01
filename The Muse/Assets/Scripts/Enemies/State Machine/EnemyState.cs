using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    protected enemyData stateData;
    protected string animBoolName;

    protected float stateStartTime;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, enemyData stateData, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.stateData = stateData;
    }

    public virtual void Enter()
    {
        stateStartTime = Time.time;
        enemy.Anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
}
