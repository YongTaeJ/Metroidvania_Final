using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventReceiver : MonoBehaviour
{
    public event Action OnAttackEnded;
    public event Action OnBulletFire;
    public event Action OnAttackStarted;
    
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
}
