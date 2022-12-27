using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyToPositionState : EnemyBaseState
{
    public EnemyToPositionState(EnemyStateMachine enemyStateMachine, EnemyStateFactory factory) : base(enemyStateMachine, factory){}

    public override void EnterState()
    {
        SetAttackDirection();
        _ctx.AttackPosition = _ctx.transform.position + _ctx.AttackDirection * 2f;
    }
    public override void UpdateState()
    {
        _ctx.transform.position = Vector2.MoveTowards(_ctx.transform.position, _ctx.AttackPosition, _ctx.Speed * Time.deltaTime);
    }
    public override void ExitState()
    {

    }
    public override void CheckStateChange()
    {
        if (_ctx.transform.position == _ctx.AttackPosition) SwitchState(_factory.Attack());
    }


    private void SetAttackDirection()
    {
        if (_ctx.transform.position.y > 0)_ctx.AttackDirection = Vector3.down;
        else if (_ctx.transform.position.y < 0) _ctx.AttackDirection = Vector3.up;
    }
}
