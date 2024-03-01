using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class Player : MonoBehaviour, IDamagable
{
    public PlayerStatus playerStatus;
    private float _hp;
    private float _mana;

    public float HP
    {
        get { return _hp; }
        set
        {
            float newValue = Mathf.Clamp(value, 0, playerStatus.Stats[PlayerStatusType.HP]);
            if (_hp != newValue)
            {
                _hp = newValue;
                OnHealthChanged?.Invoke(_hp, playerStatus.Stats[PlayerStatusType.HP]);
            }
        }
    }

    public float Mana
    {
        get { return _mana; }
        set
        {
            float newValue = Mathf.Clamp(value, 0, playerStatus.Stats[PlayerStatusType.Mana]);
            if (_mana != newValue)
            {
                _mana = newValue;
                OnManaChanged?.Invoke(_mana, playerStatus.Stats[PlayerStatusType.Mana]);
            }
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

    public bool Init = false;

    private float _shakeForce = 1f;

    //Skill
    //public List<SkillBase> _skills = new();
    public Dictionary<int, SkillBase> _skills = new Dictionary<int, SkillBase>();

    public Animator _animator;
    public Rigidbody2D _rigidbody;
    public PlayerInputController _controller;
    public PlayerInput _playerInput;
    private CinemachineImpulseSource _impulseSource;

    public event Action<float, float> OnHealthChanged;
    public event Action<float, float> OnManaChanged;
    public event Action OnPlayerDead;

    private void Awake()
    {
        GameManager.Instance.player = this;
        _controller = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();

        PlayerStatusData data = new PlayerStatusData();
        playerStatus = new PlayerStatus(data);

        Initialized();
    }

    private void Start()
    {
        bool hasItemInRange = false;

        if (ItemManager.Instance.HasItem(ItemType.Chest, 102))
        {
            hasItemInRange = true;
        }

        // 임시로 여기서 위치 데이터 받아서 위치 이동
        if (GameManager.Instance.IsFileExits())
        {
            Debug.Log("저장된 위치가 있음");
            // 세이브 데이터가 있는 경우, 그 위치로 플레이어 이동
            transform.position = new Vector3(GameManager.Instance.Data.playerPositionX, GameManager.Instance.Data.playerPositionY, GameManager.Instance.Data.playerPositionZ);
            //if (GameManager.Instance.Data.playerPositionX == 224.5f)
            //{
            //    BGM.Instance.Play("Home", true);
            //}
            //else if (GameManager.Instance.Data.playerPositionX == 301f || GameManager.Instance.Data.playerPositionX == 210f)
            //{
            //    BGM.Instance.Play("Stage1", true);
            //}
            //else if (GameManager.Instance.Data.playerPositionX == 416.5f)
            //{
            //    BGM.Instance.Play("Stage1", true);
            //}
        }
        else if (hasItemInRange == true)
        {
            transform.position = new Vector3(290f, -5f, 0f);
            //BGM.Instance.Play("Home", true);
        }
        else
        {
            transform.position = new Vector3(-18f, 0f, 0f);
            //BGM.Instance.Play("Home", true);
        }
    }

    #region Set / Init

    private void Initialized()
    {
        SetSkill();
        HP = playerStatus.Stats[PlayerStatusType.HP];
        _mana = playerStatus.Stats[PlayerStatusType.Mana];
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

    public void GetDamaged(float damage, Transform target)
    {
        if (Invincible == false && IsAlive)
        {
            SFX.Instance.PlayOneShot("PlayerHit");
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
                OnPlayerDead?.Invoke();
            }
        }
    }

    public void ConsumeMana(float amount)
    {
        Mana -= amount;
        if (Mana < 0) Mana = 0; 
    }

    public void GainMana(float amount)
    {
        Mana += amount;
    }

    public void GainHP(float amount)
    {
        HP += amount;
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
        GetComponent<SpriteRenderer>().color = originalColor;
        yield break;
         //알파값을 마지막에 원래 색으로 변경하는 코드 추가해서 버그 수정
    }


    private void OnDie()
    {
        BGM.Instance.Stop();
        BGM.Instance.Play("PlayerDie", false);
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
        _controller.CanMove = false;
        BGM.Instance.Stop();
        //BGM.Instance.Play("Home", true);
        //transform.position = new Vector3(263f, 0f, 0f);
        ContinuePosition();
        Initialized();
        UIManager.Instance.SetFixedUI(true);
        UIManager.Instance.ClosePopupUI(PopupType.GameOver);
        MapManager.Instance.LoadImage(true);
        StartCoroutine(LoadingImage());
    }

    private IEnumerator LoadingImage()
    {
        yield return new WaitForSeconds(1f);
        MapManager.Instance.LoadImage(false);
        IsAlive = true;
        _playerInput.enabled = true;
    }

    private void ContinuePosition()
    {
        if (ItemManager.Instance.HasItem(ItemType.Chest, 102))
        {
            transform.position = new Vector3(290f, -5f, 0f);
            //BGM.Instance.Play("Home", true);
        }
        else
        {
            transform.position = new Vector3(-18f, 0f, 0f);
            //BGM.Instance.Play("Home", true);
        }
    }
}
