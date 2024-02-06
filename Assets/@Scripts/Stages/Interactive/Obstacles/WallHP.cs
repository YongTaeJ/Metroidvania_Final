using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHP : WallBase
{
    [SerializeField]
    private int _wallHP = 1;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            _wallHP--;

            if (_wallHP <= 0)
            {
                BreakWall();
            }
        }
    }
}