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
        if (_cooldown > 0 || GameManager.Instance.player.Mana < ManaCost) return false; // 쿨다운이 남아있거나 마나가 부족하면 활성화하지 않습니다.

        _cooldown = Cooldown;
        GameManager.Instance.player.ConsumeMana(ManaCost);
        return true;
    }
}
