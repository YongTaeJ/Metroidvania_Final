using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHP : WallBase
{
    [SerializeField]
    private int _wallHP = 5;

    protected override void WallReact(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
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