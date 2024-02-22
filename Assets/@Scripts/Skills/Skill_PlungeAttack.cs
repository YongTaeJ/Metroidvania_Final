using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Skill_PlungeAttack : SkillBase
{
    private CinemachineImpulseSource _impulseSource;

    public override void Initialize()
    {
        base.Initialize();
        Cooldown = .2f;
        ManaCost = 35;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public override bool Activate()
    {
        if (!base.Activate())
        {
            return false;
        }

        // TODO 스킬 애니메이션
        GameManager.Instance.player._rigidbody.velocity = new Vector2(0, -transform.localScale.y * 30f);
        GameManager.Instance.player._animator.SetTrigger(AnimatorHash.Plunge);
        return true;
    }

    public void Shockwaves()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.PlungeAttack);
        CameraManager.Instance.CameraShake(_impulseSource, 2f);
        GameObject Shockwave = ResourceManager.Instance.InstantiatePrefab("PlungeAttack", pooling: true);
        Shockwave.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
    }
}
