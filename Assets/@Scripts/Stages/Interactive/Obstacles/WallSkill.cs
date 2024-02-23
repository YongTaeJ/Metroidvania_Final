using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSkill : WallBase
{
    [SerializeField]
    private int _wallHP = 1;

    protected override void WallReact(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("WallReact"));
            _wallHP = 1;
        }

        if (collision.CompareTag("SwordAuror"))
        {
            SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("WallReact"));
            _wallHP--;

            if (_wallHP <= 0)
            {
                BreakWall();
            }
        }
    }
}
