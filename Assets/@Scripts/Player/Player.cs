using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class Player : MonoBehaviour, IDamagable
{
    public int _maxHp = 10;
    public int _hp;
    public int _damage = 5;
    public int _maxMana = 100;
    public int _mana;

    public int HP
    {
        get { return _hp; }
        set
        {
            Debug.Log($"HP Changed: {_hp}/{_maxHp}");
            _hp = value;
            OnHealthChanged?.Invoke(_hp, _maxHp);
        }
    }

    public int Mana
    {
        get { return _mana; }
        set
        {
            _mana = value;
            OnManaChanged?.Invoke(_mana, _maxMana);
        }
    }

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
    public float _invincibilityTime;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            _animator.SetBool(AnimatorHash.IsAlive, value);
        }
    }

    private bool _isAlive = true;

    private float _shakeForce = 1f;

    //Skill
    //public List<SkillBase> _skills = new();
    public Dictionary<int, SkillBase> _skills = new Dictionary<int, SkillBase>();

    public Animator _animator;
    public Rigidbody2D _rigidbody;
    public PlayerInputController _controller;
    private PlayerInput _playerInput;
    private CinemachineImpulseSource _impulseSource;

    public event Action<float, float> OnHealthChanged;
    public event Action<float, float> OnManaChanged;

    private void Awake()
    {
        _controller = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        GameManager.Instance.player = this;
        Initialized();
    }

    #region Set / Init

    private void Initialized()
    {
        SetSkill();
        HP = _maxHp;
        _mana = _maxMana;
    }

    public void SetSkill()
    {
        var items = ItemManager.Instance.GetItemDict(ItemType.Skill);

        // 스킬이 늘어난다면 스킬 데이터를 만들어서 key값을 맟줘 해당하는 키의 ID와 이름을 가진 스킬을 넣을 수 있게 수정 할 수 있을듯
        foreach (var item in items.Values)
        {
            if (ItemManager.Instance.HasItem(ItemType.Skill, item.ItemData.ID))
            {
                switch (item.ItemData.ID)
                {
                    case 0:
                        if (!_skills.ContainsKey(0))
                            _skills[0] = this.AddComponent<Skill_SwordAuror>();
                        break;
                    case 1:
                        if (!_skills.ContainsKey(1))
                            _skills[1] = this.AddComponent<Skill_PlungeAttack>();
                        break;
                }
            }
        }

        foreach (var skill in _skills.Values)
        {
            skill.Initialize();
        }
    }

    #endregion

    public void GetDamaged(int damage, Transform target)
    {
        if (Invincible == false && IsAlive)
        {
            _invincibilityTime = 1f;
            Invincible = true;
            StartCoroutine(FlashPlayer());
            IsHit = true;
            HP -= damage;
            StartCoroutine(ResetHurtAnimation());
            StartCoroutine(Knockback(target));
            CameraManager.Instance.CameraShake(_impulseSource, _shakeForce);
            if (HP <= 0)
            {
                HP = 0;
                OnDie();
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

    private IEnumerator FlashPlayer()
    {
        float flashSpeed = 0.1f;
        Color originalColor = GetComponent<SpriteRenderer>().color;
        while (Invincible)
        {
            Color flashColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.1f);
            GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(flashSpeed);
            GetComponent<SpriteRenderer>().color = originalColor;
            yield return new WaitForSeconds(flashSpeed);
        }
        yield break;
        //GetComponent<SpriteRenderer>().color = originalColor; //알파값을 마지막에 원래 색으로 변경하는 코드 추가해서 버그 수정
    }


    private void OnDie()
    {
        IsAlive = false;
        _playerInput.enabled = false;
        _animator.SetTrigger(AnimatorHash.Dead);
        StartCoroutine(OnGameOverUI());
    }

    private IEnumerator OnGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.SetFixedUI(false);
        UIManager.Instance.OpenPopupUI(PopupType.GameOver);
        yield return new WaitForSeconds(1f);
    }

    public void OnContinue()
    {
        transform.position = new Vector3(263f, 0f, 0f);
        IsAlive = true;
        _playerInput.enabled = true;
        Initialized();
        UIManager.Instance.SetFixedUI(true);
        UIManager.Instance.ClosePopupUI(PopupType.GameOver);
    }
}
