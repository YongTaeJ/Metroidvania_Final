using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageButton : MonoBehaviour
{
    public static DamageButton Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public event Action<int> OnDamaged;

    public void OnClick()
    {
        OnDamaged?.Invoke(10);
    }
}
