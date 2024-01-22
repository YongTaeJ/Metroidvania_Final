using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemp : MonoBehaviour
{
    #region State

    public PlayerStateMachine StateMachine {  get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    [SerializeField]
    private PlayerData _playerData;

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerController Controller { get; private set; }
    public Rigidbody2D Rigidbody {  get; private set; }
    #endregion

    #region Check Transforms


    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection {  get; private set; }
    private Vector2 workspace;
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "Idle");
        MoveState = new PlayerWalkState(this, StateMachine, _playerData, "Walk");
        JumpState = new PlayerJumpState(this, StateMachine, _playerData, "Air");
        AirState = new PlayerAirState(this, StateMachine, _playerData, "Air");
        LandState = new PlayerLandState(this, StateMachine, _playerData, "Land");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Controller = GetComponent<PlayerController>();
        Rigidbody = GetComponent<Rigidbody2D>();

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.PlayerBaseState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.PlayerBaseState.PhysicsUpdate();
    }

    #endregion

    #region Set
    public void SetVelocityx(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check

    public bool CheckGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, _playerData.groundCheckDistance, _playerData.whatIsGround);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.PlayerBaseState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.PlayerBaseState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
