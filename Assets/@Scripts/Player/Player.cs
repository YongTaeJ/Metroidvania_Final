using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public int _maxHp = 100;
    public int _Hp;
    public int _damage = 1;

    public bool Invincible
    {
        get => _invincible;
        set
        {
            _invincible = value;
            if (_invincible)
            {
                if (_coInvincible != null) StopCoroutine(_coInvincible);
                _coInvincible = StartCoroutine(InvincibleTimer(_invincibilityTime));
            }
        }
    }

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool _invincible = false;
    private Coroutine _coInvincible;
    private float _invincibilityTime = 1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _Hp = _maxHp;
    }

    public void GetDamaged(int damage)
    {
        if (Invincible == false)
        {
            Invincible = true;

            StartCoroutine(FlashPlayer());
            _animator.SetBool(AnimatorHash.Hurt, true);
            _Hp -= damage;
            StartCoroutine(ResetHurtAnimation());

            //TODO �˹�
        }

        if (_Hp <= 0)
        {
            _Hp = 0;
            _animator.SetBool(AnimatorHash.Dead, true);
        }
    }

    private IEnumerator ResetHurtAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        _animator.SetBool(AnimatorHash.Hurt, false);
    }

    private IEnumerator InvincibleTimer(float invincibilityTime)
    {
        yield return new WaitForSeconds(invincibilityTime);
        Invincible = false;
    }

    private IEnumerator FlashPlayer()
    {
        float flashSpeed = 0.1f;

        while (Invincible)
        {
            Color originalColor = GetComponent<SpriteRenderer>().color;
            Color flashColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.1f);
            GetComponent<SpriteRenderer>().color = flashColor;

            yield return new WaitForSeconds(flashSpeed);

            GetComponent<SpriteRenderer>().color = originalColor;

            yield return new WaitForSeconds(flashSpeed);
        }
    }
}
