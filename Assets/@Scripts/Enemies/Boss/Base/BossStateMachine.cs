using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossStateMachine : EnemyStateMachine
{
    #region Fields
    protected PatternFinder _patternFinder;
    #endregion

    #region Properties
    public List<BossBaseState> AttackList {get; protected set;}
    public List<BossBaseState> PrepareList {get; protected set;}
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        Initialize();
        PatternTransition();
    }

    public void PatternTransition()
    {
        EnemyBaseState nextState = _patternFinder.GetState();
        StateTransition(nextState);
    }
}
