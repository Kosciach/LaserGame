using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFactory
{
    private EnemyStateMachine _context;

    public EnemyStateFactory(EnemyStateMachine enemyStateMachine)
    {
        _context = enemyStateMachine;
    }

    public EnemyBaseState ToPosition()
    {
        _context.CurrentStateName = "ToPosition";
        return new EnemyToPositionState(_context, this);
    }
    public EnemyBaseState Attack()
    {
        _context.CurrentStateName = "Attack";
        return new EnemyAttackState(_context, this);
    }
    public EnemyBaseState Destruction()
    {
        _context.CurrentStateName = "Destruction";
        return new EnemyDestructionState(_context, this);
    }
}
