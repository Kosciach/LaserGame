using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyStateMachine, EnemyStateFactory factory) : base(enemyStateMachine, factory){}

    public override void EnterState()
    {

    }
    public override void UpdateState()
    {
        _ctx.TimeToShoot -= _ctx.TimeSpeed * Time.deltaTime;
        if (_ctx.TimeToShoot <= 0)
        {
            GameObject.Instantiate(_ctx.EnemyProjectilePrefab, _ctx.transform.position, Quaternion.identity);
            _ctx.TimeToShoot = _ctx.TimeBetweenShots;
        }
    }
    public override void ExitState()
    {

    }
    public override void CheckStateChange()
    {

    }
}
