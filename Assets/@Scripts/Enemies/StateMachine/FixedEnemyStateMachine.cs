using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedEnemyStateMachine : EnemyStateMachine
{
    public override void Initialize()
    {
        // TODO => 구조 고민
        base.Initialize();
        StateDictionary = new()
        {
            { EnemyStateType.Idle, new EnemyIdleState(this) },
            { EnemyStateType.Wander, new EnemyIdleState(this) },
            { EnemyStateType.Chase, new EnemyRangedAttackState(this)},
            { EnemyStateType.Hurt, new EnemyHurtState(this)},
            { EnemyStateType.Dead, new EnemyDeadState(this)}
        };
    }
}
