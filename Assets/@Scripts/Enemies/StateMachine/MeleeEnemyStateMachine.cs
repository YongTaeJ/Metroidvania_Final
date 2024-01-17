using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyStateMachine : EnemyStateMachine
{
    public override void Initialize()
    {
        base.Initialize();
        StateDictionary = new()
        {
            { EnemyStateType.Idle, new EnemyIdleState(this) },
            { EnemyStateType.Wander, new EnemyWanderState(this) },
            { EnemyStateType.Chase, new EnemyChaseState(this)},
            { EnemyStateType.Attack, new EnemyMeleeAttackState(this)},
            { EnemyStateType.Hurt, new EnemyHurtState(this)},
            { EnemyStateType.Dead, new EnemyDeadState(this)}
        };
    }
}
