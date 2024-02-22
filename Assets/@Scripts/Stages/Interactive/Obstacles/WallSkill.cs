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
            _wallHP = 1;
        }
        if (collision.CompareTag("SwordAuror"))
        {
            _wallHP--;

            if (_wallHP <= 0)
            {
                BreakWall();
            }
        }
    }
}
