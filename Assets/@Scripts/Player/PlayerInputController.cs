using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    #region Properties

    public bool Iswalking
    {
        get
        {
            return _isWalking;
        }
        private set
        {
            _isWalking = value;
            _animator.SetBool(AnimatorHash.Walk, value);
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            _isFacingRight = value;
        }
    }

    public bool IsAttacking
    {
        get
        {
            return _isAttacking;
        }
        private set
        {
            _isAttacking = value;
            _animator.SetBool(AnimatorHash.IsAttacking, value);
        }
    }
    #endregion

    #region Fileds

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private TouchingDirection _touchingDirection;
    private Player _player;
    private float _speed = 10f;
    private Vector2 _moveInput;

    private bool _isWalking = false;
    private bool _isAttacking = false;
    private bool _isFirstAttack = true;
    private bool _isFacingRight = true;

    //Jump
    private float _jumpPower = 20f;
    private bool _doubleJump = false;

    //Gravity
    private float _baseGravity = 4f;
    private float _maxFallSpeed = 20f;
    private float _fallSpeedMultiplier = 2f;

    //Wall Slide
    private float _wallSlideSpeed = 0f;
    private bool _isWallSliding = false;
    public bool IsWallSliding { get 
        {
            return _isWallSliding; 
        } 
        private set
        {
            _isWallSliding = value;
            _animator.SetBool(AnimatorHash.WallSliding, value);
        }
    }

    // WallJump
    private bool _isWallJumping;
    private float _wallJumpDirection;
    private float _wallJumpTime = 0.25f;
    private float _wallJumpTimer;
    private Vector2 _wallJumpPower = new Vector2(5f, 15f);

    // Dash
    private bool _canDash = true;
    private bool _isDashing;
    private float _dashPower = 15f;
    private float _dashTime = 0.2f;
    private int _maxDash = 1;
    public int _dashCount;

    // Action
    public event Action OnInteraction;

    // Coroutine
    private Coroutine _coMoveCamera;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _touchingDirection = GetComponent<TouchingDirection>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();

    }

    private void FixedUpdate()
    {
        GroundCheck();
        WallSlide();
        WallJump();

        if (!_isWallJumping && !_isDashing && !_player.IsHit)
        {
            _rigidbody.velocity = new Vector2(_moveInput.x * _speed, _rigidbody.velocity.y);
            Flip();
        }

        if (!_isDashing)
        {
            Gravity();
        }

        _animator.SetFloat(AnimatorHash.yVelocity, _rigidbody.velocity.y);
    }

    #endregion

    #region Movement

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Iswalking = _moveInput != Vector2.zero;
    }

    /// <summary>
    /// 연출 전용 옵션
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector2 direction)
    {
        _moveInput = direction;
        Iswalking = _moveInput != Vector2.zero;
    }

    public Vector2 MoveInput()
    {
        return _moveInput;
    }

    private void Flip()
    {
        if (_isAttacking == false)
        {
            if (_isFacingRight && _moveInput.x < 0 || !_isFacingRight && _moveInput.x > 0)
            {
                _isFacingRight = !_isFacingRight;
                Vector3 Is = transform.localScale;
                Is.x *= -1f;
                transform.localScale = Is;
            }
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        if (_moveInput.x == 0 && _touchingDirection.IsGrounded)
        {
            if (context.performed)
            {
                if (_coMoveCamera != null)
                    StopCoroutine(_coMoveCamera);

                _coMoveCamera = StartCoroutine(MoveCameraDelay(direction));
            }
            else if (context.canceled)
            {
                if (_coMoveCamera != null)
                    StopCoroutine(_coMoveCamera);

                CameraManager.Instance.ResetCamera();
            }
        }
    }

    private IEnumerator MoveCameraDelay(Vector2 direction)
    {
        yield return new WaitForSeconds(0.5f);
        CameraManager.Instance.MoveCamera(direction);
    }

    private bool CanWallJump()
    {
        return _wallJumpTimer > 0f && _touchingDirection.IsWall;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (context.started && !_isDashing)
            {
                if (!_isWallJumping && _doubleJump || _touchingDirection.IsGrounded)
                {
                    _animator.SetTrigger(AnimatorHash.Jump);
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
                    if(ItemManager.Instance.HasItem(ItemType.Equipment, 2))
                    {
                        _doubleJump = !_doubleJump;
                    }
                }
            }
            else if (context.canceled)
            {
                if (!_isWallJumping)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
                }
            }
            
            //Wall Jump
            if (context.started && _wallJumpTimer > 0f)
            {
                if (!_isWallJumping && _touchingDirection.IsWall)
                {
                    isWallSlideEffect = false;
                    StopCoroutine("WallSlideEffect");
                    _isWallSliding = false;
                    _isWallJumping = true;
                    _rigidbody.velocity = new Vector2(_wallJumpDirection * _wallJumpPower.x, _wallJumpPower.y);
                    _wallJumpTimer = 0f;
                    _animator.SetTrigger(AnimatorHash.WallJump);
                    if (transform.localScale.x != _wallJumpDirection)
                    {
                        _isFacingRight = !_isFacingRight;
                        Vector3 Is = transform.localScale;
                        Is.x *= -1f;
                        transform.localScale = Is;
                    }

                    Invoke(nameof(CancelWallJump), _wallJumpTime + 0.1f);
                }
            }
            else if (context.canceled)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }
        }

    }

    private void WallJump()
    {
        if (enabled)
        {
            if (IsWallSliding)
            {
                _isWallJumping = false;
                _wallJumpDirection = -transform.localScale.x;
                _wallJumpTimer = _wallJumpTime;

                CancelInvoke(nameof(CancelWallJump));
            }
            else if (_wallJumpTimer > 0f)
            {
                _wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    private void CancelWallJump()
    {
        _isWallJumping = false;
    }

    private bool isWallSlideEffect = false;

    private void WallSlide()
    {
        if (enabled)
        {
            bool isCollidingWithWall = CheckWallCollision();
            if (isCollidingWithWall & !_touchingDirection.IsGrounded & _touchingDirection.IsWall & _moveInput.x != 0 & _rigidbody.velocity.y <= 0 && !_isWallJumping && ItemManager.Instance.HasItem(ItemType.Equipment, 1))
            {
                IsWallSliding = true;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -_wallSlideSpeed));
                if (!isWallSlideEffect)
                {
                    isWallSlideEffect = true;
                    StartCoroutine(WallSlideEffect());
                }
            }
            else
            {
                IsWallSliding = false;
                if (isWallSlideEffect)
                {
                    isWallSlideEffect = false;
                    StopCoroutine("WallSlideEffect");
                }
            }
        }
    }

    private bool CheckWallCollision()
    {
        Vector2 boxSize = new Vector2(1f, 0.5f); 
        Vector2 boxPosition = new Vector2(transform.position.x + (Mathf.Sign(_moveInput.x) * boxSize.x / 2), transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxPosition, boxSize, 0, _touchingDirection.groundLayerMask);

        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                return true; // 벽과 충돌하는 콜라이더가 하나라도 있다면 true 반환
            }
        }
        return false; // 충돌하는 벽이 없다면 false 반환
    }

    private IEnumerator WallSlideEffect()
    {
        while (isWallSlideEffect)
        {
            
            yield return new WaitForSeconds(0.1f);
            if (!isWallSlideEffect || !_isWallSliding)
            {
                yield break;
            }
            GameObject wallSlideParticle = ResourceManager.Instance.InstantiatePrefab("WallSlideParticle", pooling: true);
        
            float wallSlideDirection = IsFacingRight ? 0.5f : -0.5f;
            Vector2 point = new Vector2(transform.position.x + wallSlideDirection, transform.position.y);
            wallSlideParticle.transform.position = point;

            float wallSlideScale = IsFacingRight ? -1 : 1;
            Vector3 scale = new Vector3(wallSlideScale, 1, 1);
            wallSlideParticle.transform.localScale = scale;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (context.performed && !IsAttacking && !_player.IsHit &&!_isDashing)
            {
                IsAttacking = true;
                if (_isFirstAttack)
                {
                    _animator.SetTrigger(AnimatorHash.Attack);
                    GameObject attackEffect = ResourceManager.Instance.InstantiatePrefab("AttackEffect_Temp", pooling : true);
                    var playerAttack = attackEffect.GetComponent<PlayerAttack>();
                    
                    float attackDirection = IsFacingRight ? 1f : -1f;
                    Vector2 attackPoint = new Vector2(transform.position.x + (attackDirection * 1.5f), transform.position.y + 0.5f);

                    float attackscale = IsFacingRight ? 1f : -1f;
                    Vector2 scale = new Vector2(attackscale, 1);

                    attackEffect.transform.localScale = scale;
                    attackEffect.transform.position = attackPoint;

                    playerAttack.hasAttacked = false;

                    _isFirstAttack = false;

                }
                else if (!_isFirstAttack)
                {
                    _animator.SetTrigger(AnimatorHash.Attack2);
                    GameObject attackEffect2 = ResourceManager.Instance.InstantiatePrefab("AttackEffect_Temp_2", pooling: true);
                    var playerAttack = attackEffect2.GetComponent<PlayerAttack>();
                    
                    float attackDirection = IsFacingRight ? 1f : -1f;
                    Vector2 attackPoint = new Vector2(transform.position.x + (attackDirection * 1.5f), transform.position.y + 0.1f);

                    float attackscale = IsFacingRight ? 1f : -1f;
                    Vector2 scale = new Vector2(attackscale, 1);

                    attackEffect2.transform.localScale = scale;
                    attackEffect2.transform.position = attackPoint;

                    playerAttack.hasAttacked = false;

                    _isFirstAttack = true;
                }
                StartCoroutine(ResetAttackAnimation());
            }
        }
    }

    // TODO 리펙토링 필요해 보임
    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(_player.playerStatus.Stats[PlayerStatusType.AttackSpeed]);
        IsAttacking = false;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (!IsAttacking && !_player.IsHit)
        {
            if (context.performed && _canDash == true && ItemManager.Instance.HasItem(ItemType.Equipment, 0))
            {
                StartCoroutine(CoDash());
            }
        }
    }

    private IEnumerator CoDash()
    {
        if (_dashCount > 0)
        {
            //_animator.SetTrigger(AnimatorHash.Dash);
            _canDash = false;
            _isDashing = true;

            // 대쉬에 무적 필요한지?
            _player._invincibilityTime = _dashTime;
            _player.Invincible = true;

            float originalGravity = _baseGravity;
            _rigidbody.gravityScale = 0f;

            List<GameObject> dashEffects = new List<GameObject>();

            _rigidbody.velocity = new Vector2(transform.localScale.x * _dashPower, 0f);

            float elapsedTime = 0f;
            while (elapsedTime < _dashTime)
            {
                var dashEffect = ResourceManager.Instance.InstantiatePrefab("DashEffect", pooling: true);
                dashEffect.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
                dashEffects.Add(dashEffect);
                yield return new WaitForSeconds(0.04f);
                elapsedTime += 0.04f;
            }
            _rigidbody.gravityScale = originalGravity;
            _isDashing = false;
            _dashCount--;
            yield return new WaitForSeconds(0.1f);
            _canDash = true;
        }
    }

    public void Skill(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (!Input.GetKey(KeyCode.DownArrow) && context.started && ItemManager.Instance.HasItem(ItemType.Skill, 0))
            {
                _player._skills[0].Activate();
            }

            if (Input.GetKey(KeyCode.DownArrow) && !_touchingDirection.IsGrounded && context.started && ItemManager.Instance.HasItem(ItemType.Skill, 1))
            {
                _player._skills[1].Activate();

                _player.Invincible = true;
            }
        }
    }

    public void Interaction(InputAction.CallbackContext context)
    {
        if (enabled && context.performed)
        {
            OnInteraction?.Invoke();
        }
    }

    private void Gravity()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = _baseGravity * _fallSpeedMultiplier;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -_maxFallSpeed));
        }
        else
        {
            _rigidbody.gravityScale = _baseGravity;
        }
    }

    #endregion

    #region Check

    private void GroundCheck()
    {
        if (_touchingDirection.IsGrounded)
        {
            _dashCount = _maxDash;

            if(ItemManager.Instance.HasItem(ItemType.Equipment, 2))
            {
                _doubleJump = true;
            }
        }
    }

    #endregion

    #region UI Input

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIManager.Instance.OpenPopupUI(PopupType.Inventory);
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //if 켜진 UI가 없다면
            UIManager.Instance.OpenPopupUI(PopupType.Pause);
            //else if 켜진 Popup이 있다면 UI SetActive(false)
        }
    }

    #endregion
}
