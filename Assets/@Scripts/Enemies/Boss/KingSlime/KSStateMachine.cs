
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
            {EnemyStateType.Dead, new EnemyDeadState(this)}
        };

        AttackList = new()
        {
            new KSBreathAttackState(this),
            new KSJumpAttackState(this),
            new KSSummonAttackState(this),
            new KSRangedAttackState(this)
        };

        PrepareList = new()
        {
            new BossChaseState(this),
            new BossIdleState(this)
        };

        _patternFinder = new PatternFinder(this);
    }
}
