
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType
{
    None,
    Idle,
    Chase,
    Attack
}

public class EnemyStateMachine : StateMachine<EnemyBaseState>
{
    #region Properties
    public Dictionary<EnemyStateType, EnemyBaseState> StateDictionary {get; private set;}
    public Rigidbody2D Rigidbody {get; private set;}
    public EnemyData EnemyData {get; private set;}
    public PlayerFinder PlayerFinder {get; private set;}
    public Animator Animator {get; private set;}
    #endregion

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerFinder = GetComponentInChildren<PlayerFinder>();
        Animator = GetComponentInChildren<Animator>();

        // TOOD => 임시 데이터 처리. 추후 개선 필요!!
        EnemyData = new EnemyData()
        {
            Speed = 1f,
            Damage = 3f
        };

        StateDictionary = new()
        {
            { EnemyStateType.Idle, new EnemyIdleState(this) },
            { EnemyStateType.Chase, new EnemyChaseState(this)},
            { EnemyStateType.Attack, new EnemyAttackState(this)}
        };

        StateTransition(StateDictionary[EnemyStateType.Idle]);
    }
}