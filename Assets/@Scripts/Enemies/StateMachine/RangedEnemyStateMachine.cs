using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyStateMachine : EnemyStateMachine
{
    public override void Initialize()
    {
        base.Initialize();
        StateDictionary = new()
        {
            { EnemyStateType.Idle, new EnemyIdleState(this) },
            { EnemyStateType.Wander, new EnemyWanderState(this) },
            { EnemyStateType.Chase, new EnemyChaseState(this)},
            { EnemyStateType.Attack, new EnemyRangedAttackState(this)}
        };
    }
}
