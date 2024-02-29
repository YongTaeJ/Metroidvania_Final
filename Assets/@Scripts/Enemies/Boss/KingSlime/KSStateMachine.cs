
using UnityEngine.UIElements;

public class KSStateMachine : BossStateMachine
{
    public override void Initialize()
    {
        base.Initialize();

        // Link To EnemyHitSystem.
        StateDictionary = new()
        {
            {EnemyStateType.Hurt, new KSHurtState(this)},
            {EnemyStateType.Dead, new KSDeadState(this)}
        };

        AttackList = new()
        {
            new KSBreathAttackState(this),
            new KSJumpAttackState(this),
            new KSSummonAttackState(this),
            new KSRangedAttackState(this),
            new KSDoubleAttackState(this),
            new KSRangedAttackState_Melee(this),
            new KSBreathAttackState_Ranged(this),
            new KSDoubleAttackState_Melee(this)
        };

        PrepareList = new()
        {
            new BossChaseState(this),
            new BossIdleState(this)
        };

        _patternFinder = new PatternFinder(this);
    }
}
