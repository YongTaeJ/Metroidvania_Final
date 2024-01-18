using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공용으로 사용하는 애니메이터의 해시값을 저장하는 전역 클래스입니다.
/// </summary>
public static class AnimatorHash
{
    public static readonly int Walk = Animator.StringToHash("Walk");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int Dead = Animator.StringToHash("Dead");
    public static readonly int Hurt = Animator.StringToHash("Hurt");
    public static readonly int Jump = Animator.StringToHash("Jump");
    public static readonly int yVelocity = Animator.StringToHash("yVelocity");
    public static readonly int WallSliding = Animator.StringToHash("WallSliding");
    public static readonly int WallJump = Animator.StringToHash("WallJump");
    public static readonly int Dash = Animator.StringToHash("Dash");
    public static readonly int IsGrounded = Animator.StringToHash("Ground");
    public static readonly int IsWall = Animator.StringToHash("Wall");
    public static readonly int IsCeiling = Animator.StringToHash("Ceiling");
}