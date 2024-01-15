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
}