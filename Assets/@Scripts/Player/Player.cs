using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public int _maxHp = 10;
    public int _Hp;
    public int _damage = 5;
    public bool IsHit
    {
        get
        {
            return _animator.GetBool(AnimatorHash.Hurt);
        }
        private set
        {
            _animator.SetBool(AnimatorHash.Hurt, value);
        }
    }
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
    private bool _invincible = false;
    private Coroutine _coInvincible;
    private float _invincibilityTime = 1f;

    public Animator _animator;
    public Rigidbody2D _rigidbody;
    public PlayerInputController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _Hp = _maxHp;
        GameManager.Instance.player = this;
    }

    public void GetDamaged(int damage, Transform target)
    {
        if (Invincible == false)
        {
            Invincible = true;
            StartCoroutine(FlashPlayer());
            IsHit = true;
            GameManager.Instance.player._Hp -= damage;
            StartCoroutine(ResetHurtAnimation());
            StartCoroutine(Knockback(target));

            if (GameManager.Instance.player._Hp <= 0)
            {
                GameManager.Instance.player._Hp = 0;
                _animator.SetBool(AnimatorHash.Dead, true);
            }
        }
    }

    private IEnumerator Knockback(Transform target)
    {
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        Vector2 knockbackDirection = new Vector2(-direction, 1f).normalized;
        _rigidbody.velocity = knockbackDirection * 5f;
        yield return new WaitForSeconds(0.2f);
        _rigidbody.velocity = Vector2.zero;
    }

    private IEnumerator ResetHurtAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        IsHit = false;
    }

    private IEnumerator InvincibleTimer(float invincibilityTime)
    {
        yield return new WaitForSeconds(invincibilityTime);
        Invincible = false;
    }

    // TODO 연속해서 맞을경우 알파값이 낮은 상태로 고정되는 버그 있음 애니메이션에 적용해서 이부분은 없앨수 있을듯
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
