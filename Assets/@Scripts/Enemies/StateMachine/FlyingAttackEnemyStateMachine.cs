using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackEnemyStateMachine : EnemyStateMachine
{
    public override void Initialize()
    {
        base.Initialize();
        StateDictionary = new()
        {
            { EnemyStateType.Idle, new FlyingEnemyIdleState(this) },
            { EnemyStateType.Chase, new FlyingEnemyChaseState(this)},
            { EnemyStateType.Attack, new EnemyRangedAttackState(this)},
            { EnemyStateType.Hurt, new EnemyHurtState(this)},
            { EnemyStateType.Dead, new EnemyDeadState(this)}
        };
    }

}
