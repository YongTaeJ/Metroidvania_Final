using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        LaunchProjectile();

        return true;
    }

    private void LaunchProjectile()
    {
        //if (GameManager.Instance.player == null)
        //{
        //    Debug.LogWarning("Player object not found in GameManager.Instance.player.");
        //    return;
        //}

        //Quaternion rotation = Quaternion.Euler(0, 0, GameManager.Instance.player.transform.localScale.x > 0 ? 0 : 180);

        GameObject projectile = Instantiate(_swordAuror, transform.position, Quaternion.identity);

        //GameObject projectile = Instantiate(_swordAuror, transform.position, rotation);

        //Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        //if (projectileRigidbody != null)
        //{
        //    float speed = 1000f;
        //    projectileRigidbody.velocity = new Vector2(GameManager.Instance.player.transform.localScale.x > 0 ? speed : -speed, 0f);
        //}
        //else
        //{
        //    Debug.LogWarning("Rigidbody2D component not found on projectile.");
        //    // 또는 필요한 처리 추가
        //}
    }
}
