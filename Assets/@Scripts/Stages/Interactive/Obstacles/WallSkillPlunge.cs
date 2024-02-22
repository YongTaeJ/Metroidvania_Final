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
            Debug.Log("Attacked");
            _wallHP = 1;
        }

        if (collision.CompareTag("PlungeAttack"))
        {
            _wallHP--;

            Debug.Log("Plunge");

            if (_wallHP <= 0)
            {
                BreakWall();
            }
        }
    }
}
