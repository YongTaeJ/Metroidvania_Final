using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventReceiver : MonoBehaviour
{
    public event Action OnAttackEnded;
    public void Animation_AttackEnded()
    {
        OnAttackEnded?.Invoke();
    }
}
