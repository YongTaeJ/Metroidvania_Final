using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{
    
    public float Cooldown { get; protected set; }
    public int ManaCost { get; protected set; }
    private float _cooldown;

    protected virtual void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }
    }

    public virtual void Initialize()
    {
      
    }

    public virtual bool Activate()
    {
        if (_cooldown > 0) return false;
        _cooldown = Cooldown;

        return true;
    }
}
