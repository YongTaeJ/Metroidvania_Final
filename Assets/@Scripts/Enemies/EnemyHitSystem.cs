using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyHitSystem : MonoBehaviour, IDamagable
{
    #region FlashFields
    private SpriteRenderer _spriteRenderer;
    private Material _flashMaterial;
    private Material _originalMaterial;
    private float _flashDuration;
    private Coroutine _flashCoroutine;
    #endregion

    #region Fields

    private PlayerFinder _playerFinder;
    private EnemyStateMachine _stateMachine;
    private int _maxEndurance;
    private float _maxHP;
    private float _currentHP;
    private int _currentEndurance;
    private bool _isInvincible;
    #endregion

    public void Initialize(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _maxEndurance = stateMachine.EnemyData.HitEndurance;
        _maxHP = stateMachine.EnemyData.HP;

        _currentEndurance = _maxEndurance;
        _currentHP = _maxHP;

        _playerFinder = stateMachine.PlayerFinder;
        _isInvincible = false;

        _flashMaterial = Resources.Load<Material>("Materials/FlashMaterial");
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
        _flashDuration = 0.15f;
    }

    public void GetDamaged(float damage, Transform target)
    {
        if(_isInvincible) return;
        
        _currentEndurance--;
        _currentHP -= damage;
        
        Flash();

        if(_currentHP <= 0)
        {
            StopAllCoroutines();
            _spriteRenderer.material = _originalMaterial;
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Dead]);
            return;
        }

        if(_currentEndurance <= 0)
        {
            KnockBack();
            _currentEndurance = _maxEndurance;
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Hurt]);
            return;
        }
    }

    private void KnockBack()
    {
        if(_playerFinder.CurrentTransform == null) return;

        float dist = _playerFinder.CurrentTransform.position.x - transform.position.x;
        float direction = dist > 0 ? -1 : 1;

        transform.DOMoveX(transform.position.x + direction * 0.2f , 0.3f);
    }

    public void ResetHPCondition()
    {
        _currentHP = _maxHP;
        _currentEndurance = _maxEndurance;
        _isInvincible = false;
    }

    private void Flash()
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
        }
        _flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        _spriteRenderer.material = _flashMaterial;

        yield return new WaitForSeconds(_flashDuration);

        _spriteRenderer.material = _originalMaterial;

        _flashCoroutine = null;
    }
}
