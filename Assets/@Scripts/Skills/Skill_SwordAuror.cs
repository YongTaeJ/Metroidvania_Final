using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_SwordAuror : SkillBase
{
    //임시
    public GameObject _swordAuror;
    public override void Initialize()
    {
        base.Initialize();
        Cooldown = 5f;
    }

    public override bool Activate()
    {
        if (!base.Activate())
        {
            return false;
        }
        GameManager.Instance.player._animator.SetTrigger(AnimatorHash.Skill);
        LaunchProjectile();
        return true;
    }

    private void LaunchProjectile()
    {
        if (GameManager.Instance.player == null) return;

        Quaternion rotation = Quaternion.Euler(0, 0, GameManager.Instance.player.transform.localScale.x > 0 ? 0 : 180);

        GameObject projectile = Instantiate(_swordAuror, transform.position, rotation);

        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        if (projectileRigidbody != null)
        {
            float speed = 30f;
            projectileRigidbody.velocity = new Vector2(GameManager.Instance.player.transform.localScale.x > 0 ? speed : -speed, 0f);
        }
    }
}
