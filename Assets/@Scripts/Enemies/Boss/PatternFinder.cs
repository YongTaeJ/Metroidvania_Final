using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BossPatternType
{
    None,
    Melee,
    Ranged,
    Random,
    Special
}
public class PatternFinder
{
    #region Fields
    private bool _isAttacked;
    private int _maxSpecialCount;
    private int _currentSpecialCount;
    private float _attackDistance;
    private Transform _bossTransform;
    private Transform _playerTransform;
    private Animator _animator;
    private Dictionary<BossPatternType, List<BossBaseState>> _attackPatterns;
    private List<BossBaseState> _preparePatterns;
    #endregion

    public PatternFinder(BossStateMachine stateMachine)
    {
        _isAttacked = false;
        _maxSpecialCount = 5;
        _currentSpecialCount = 0;

        _attackDistance = stateMachine.EnemyData.AttackDistance;
        _bossTransform = stateMachine.transform;
        _playerTransform = stateMachine.PlayerFinder.CurrentTransform;
        _animator = stateMachine.Animator;

        SetAttackPatterns(stateMachine.AttackList);
        _preparePatterns = stateMachine.PrepareList;
    }

    private void SetAttackPatterns(List<BossBaseState> patterns)
    {
        foreach(var pattern in patterns)
        {
            _attackPatterns[pattern.BossPatternType].Add(pattern);
        }
    }

    public EnemyBaseState GetState()
    {
        if(_isAttacked)
        {
            _isAttacked = false;
            return GetAttackPattern();
        }
        else
        {
            _isAttacked = true;
            return GetPrepaerPattern();
        }
    }

    private EnemyBaseState GetAttackPattern()
    {
        _animator.SetTrigger(AnimatorHash.Attack);

        if(_currentSpecialCount >= _maxSpecialCount && _attackPatterns.ContainsKey(BossPatternType.Special))
        {
            _currentSpecialCount = 0;
            return GetRandomPattern(_attackPatterns[BossPatternType.Special]);
        }

        _currentSpecialCount++;

        int rand = Random.Range(0,100);
        if(rand < 20 && _attackPatterns.ContainsKey(BossPatternType.Random))
        {
            return GetRandomPattern(_attackPatterns[BossPatternType.Random]);
        }

        float distance = Vector2.Distance(_playerTransform.position, _bossTransform.position);

        if(distance < _attackDistance)
        {
            return GetRandomPattern(_attackPatterns[BossPatternType.Melee]);
        }
        else
        {
            return GetRandomPattern(_attackPatterns[BossPatternType.Ranged]);
        }
    }
    private EnemyBaseState GetPrepaerPattern()
    {
        _animator.SetTrigger(AnimatorHash.Prepare);
        return GetRandomPattern(_preparePatterns);
    }

    private EnemyBaseState GetRandomPattern(List<BossBaseState> patterns)
    {
        int count = patterns.Count;
        int rand = Random.Range(0, count);
        return patterns[rand];
    }
}
