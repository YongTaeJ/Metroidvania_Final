using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNormal : WallBase
{
    protected override void WallReact(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BreakWall();
        }
    }
}
