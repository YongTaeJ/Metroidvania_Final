using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_SwordAuror : SkillBase
{
    private GameObject _swordAurorPrefab;
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
        SwordAuror();
        return true;
    }

    private void SwordAuror()
    {
        if (GameManager.Instance.player == null) return;
        _swordAurorPrefab = Resources.Load<GameObject>("Skills/SwordAuror");
        GameObject swordAurorPrefab = ResourceManager.Instance.InstantiatePrefab("SwordAuror", pooling: true);
        float swordAurorScale = GameManager.Instance.player._controller.IsFacingRight ? 1 : -1;
        swordAurorPrefab.transform.localScale = new Vector2(swordAurorScale, 1);
        swordAurorPrefab.transform.position = transform.position;
        Rigidbody2D swordAurorRigidbody = swordAurorPrefab.GetComponent<Rigidbody2D>();

        if (swordAurorRigidbody != null)
        {
            float speed = 30f;
            swordAurorRigidbody.velocity = new Vector2(GameManager.Instance.player.transform.localScale.x > 0 ? speed : -speed, 0f);
        }
    }
}
