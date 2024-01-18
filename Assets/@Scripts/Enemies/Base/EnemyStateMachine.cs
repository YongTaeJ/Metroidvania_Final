
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType
{
    None,
    Idle,
    Wander,
    Chase,
    Attack,
    Hurt,
    Dead
}

public abstract class EnemyStateMachine : StateMachine<EnemyBaseState>
{
    #region ID
    [SerializeField] private int ID;
    #endregion

    #region Properties
    public Dictionary<EnemyStateType, EnemyBaseState> StateDictionary {get; protected set;}
    public Rigidbody2D Rigidbody {get; private set;}
    public EnemyData EnemyData {get; private set;}
    public PlayerFinder PlayerFinder {get; private set;}
    public Animator Animator {get; private set;}
    public EnemyAnimationEventReceiver EventReceiver {get; private set;}
    #endregion

    #region Monobehaviour

    private void Awake()
    {
        Initialize();
        StateTransition(StateDictionary[EnemyStateType.Idle]);
    }

    private void Start()
    {
        new EnemyHitSystem(this);
    }

    #endregion

    public virtual void Initialize()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

        EventReceiver = GetComponentInChildren<EnemyAnimationEventReceiver>();
        PlayerFinder = GetComponentInChildren<PlayerFinder>();
        Animator = GetComponentInChildren<Animator>();

        EnemyData = EnemyDataManager.Instance.GetEnemyData(ID);

        
    }
}