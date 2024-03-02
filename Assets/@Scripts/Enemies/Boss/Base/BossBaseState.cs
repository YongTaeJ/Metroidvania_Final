using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBaseState : EnemyBaseState
{
    public List<BossPatternType> BossPatternTypes {get; protected set;}
    protected BossBaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
}
