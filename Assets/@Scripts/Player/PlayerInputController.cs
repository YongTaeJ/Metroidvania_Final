using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharacterController
{
    #region Properties

    private Vector2 _movementDirection = Vector2.zero;
    private Vector2 _aimDirection = Vector2.right;
    private Rigidbody2D _rigidbody;
    private Player _player;
    private float _timeSinceLastAttack = float.MaxValue;
    protected bool _isAttacking { get; set; }
    private bool _isGrounded;
    private float _jumpPowar = 5f;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _isGrounded = true;
    }

    private void Start()
    {
        OnMoveEvent += Move;
        OnLookEvent += OnAim;
    }

    protected override void Update()
    {
        base.Update();
        //AttackDelay();
    }

    #endregion


    #region Move
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    private void FixedUpdate()
    {
        ApplyMovment(_movementDirection);
        CheckGrounded();
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovment(Vector2 direction)
    {
        direction *= 5f; //플레이어 speed
        //direction += KnockbackDirection;
        _rigidbody.velocity = direction;
    }

    //public Vector2 KnockbackDirection = Vector2.zero;
    #endregion

    #region Look
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        CallLookEvent(newAim);
    }

    public void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection.normalized;
    }
    #endregion

    #region Jump

    public void OnJump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpPowar, ForceMode2D.Impulse);
            _isGrounded = false; // 땅에 있는 상태를 업데이트합니다.
            Debug.Log("Jump");
        }
    }

    private void CheckGrounded()
    {
        // 플레이어가 땅에 있는지를 결정하기 위해 레이캐스트 또는 다른 땅 체크 메커니즘을 수행합니다.
        // 단순화된 예로, 플레이어의 y-위치가 땅과 가까우면 땅에 있는 것으로 가정합니다.
        float groundCheckDistance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);

        // 땅 체크 결과를 기반으로 땅에 있는지 여부를 업데이트합니다.
        _isGrounded = hit.collider != null;
    }
    #endregion

    #region Dash

    public void OnDash()
    {
        Debug.Log("Dash");

    }

    #endregion

    #region Attack

    public void OnAttack()
    {
        Debug.Log("Attack");

    }

    #endregion
}
