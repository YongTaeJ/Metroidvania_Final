using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventReceiver : MonoBehaviour
{
    public event Action OnAttackEnded;
    public event Action OnBulletFire;
    public event Action OnAttackStarted;
    public event Action OnDeadEnded;
    public event Action OnHurtEnded;
    
    private void Animation_AttackEnded()
    {
        OnAttackEnded?.Invoke();
    }

    private void Animation_BulletFire()
    {
        OnBulletFire?.Invoke();
    }

    private void Animation_AttackStarted()
    {
        OnAttackStarted?.Invoke();
    }

    private void Animation_DeadEnded()
    {
        OnDeadEnded?.Invoke();
    }

    private void Animation_HurtEnded()
    {
        OnHurtEnded?.Invoke();
    }
}
