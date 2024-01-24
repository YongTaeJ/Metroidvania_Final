using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHStateMachine : BossStateMachine
{
    public override void Initialize()
    {
        base.Initialize();

        // Link To EnemyHitSystem.
        StateDictionary = new()
        {
            {EnemyStateType.Hurt, new VHHurtState(this)},
            {EnemyStateType.Dead, new VHDeadState(this)}
        };

        AttackList = new()
        {
            new VHBasicAttack1State(this),
            new VHBasicAttack2State(this),
            new VHDashAttackState(this),
            new VHLeapAttackState(this),
            new VHSpinAttackState(this),
            new VHSpecialAttackState(this)
        };

        PrepareList = new()
        {
            new VHIdleState(this),
            new VHChaseState(this)
        };

        _patternFinder = new PatternFinder(this);
    }
}
