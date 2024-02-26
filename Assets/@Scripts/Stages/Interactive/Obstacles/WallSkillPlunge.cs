using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSkillPlunge : WallBase
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

        if (collision.CompareTag("PlungeAttack"))
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
