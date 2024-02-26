
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
    public ObjectFlip ObjectFlip {get; private set;}
    public Collider2D Collider { get; private set;}
    #endregion

    #region Monobehaviour

    protected virtual void Start()
    {
        Initialize();
        StateTransition(StateDictionary[EnemyStateType.Idle]);
    }

    #endregion

    public virtual void Initialize()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();

        EventReceiver = GetComponentInChildren<EnemyAnimationEventReceiver>();
        PlayerFinder = GetComponentInChildren<PlayerFinder>();
        Animator = GetComponentInChildren<Animator>();

        ObjectFlip = new ObjectFlip(transform);

        EnemyData = EnemyDataManager.Instance.GetEnemyData(ID);
        
        GetComponent<EnemyHitSystem>().Initialize(this);

        var attackComponents = transform.Find("Sprite").GetComponentsInChildren<IHasDamage>();
        foreach(var component in attackComponents)
        {
            component.Initialize(EnemyData.Damage);
        }
    }

    public void ResetMonster()
    {
        GetComponent<EnemyHitSystem>().ResetHPCondition();
        var attackComponents = transform.Find("Sprite").GetComponentsInChildren<EnemyBodyAttackSystem>();
        foreach (var component in attackComponents)
        {
            component._collider.enabled = true;
        }
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        StateTransition(StateDictionary[EnemyStateType.Idle]);
    }
}