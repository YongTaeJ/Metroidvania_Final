using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallBase : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        WallReact(collision);
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // interact end
        }
    }
    protected virtual void BreakWall()
    {
        // 특정 조건에 따라서 작동하게
        GameObject.Destroy(this.gameObject);
    }

    protected abstract void WallReact(Collider2D collision);
}
