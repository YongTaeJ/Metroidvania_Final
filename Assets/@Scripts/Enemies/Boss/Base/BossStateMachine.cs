using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPatternType
{
    None,
    Melee,
    Ranged,
    Random,
    Special
}
public abstract class BossStateMachine : StateMachine<BossBaseState>
{
    #region Fields
    private PatternFinder _patternFinder;
    #endregion

    #region Properties

    public List<BossBaseState> AttackList {get; protected set;}
    public List<BossBaseState> IdleList {get; protected set;}
    #endregion


    #region Monobehaviour

    private void Awake()
    {
        Initialize();
        _patternFinder = new PatternFinder();
        // StateTransition(StateDictionary[EnemyStateType.Idle]);
    }
    #endregion

    public virtual void Initialize()
    {
        _patternFinder = new PatternFinder(); 
    }
}
