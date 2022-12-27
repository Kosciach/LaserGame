using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    protected EnemyStateMachine _ctx;
    protected EnemyStateFactory _factory;
    public EnemyBaseState(EnemyStateMachine enemyStateMachine, EnemyStateFactory factory)
    {
        _ctx = enemyStateMachine;
        _factory = factory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckStateChange();

    protected void SwitchState(EnemyBaseState newState)
    {
        ExitState();
        newState.EnterState();
        _ctx.CurrentState = newState;
    }
}
