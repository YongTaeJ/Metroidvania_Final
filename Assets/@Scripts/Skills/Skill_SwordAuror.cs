using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SwordAuror : SkillBase
{
    //임시
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

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
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        if (projectileRb != null)
        {
            projectileRb.velocity = transform.right * projectileSpeed;
        }
    }
}
