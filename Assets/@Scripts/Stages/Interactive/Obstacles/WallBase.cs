using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallBase : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        WallReact(collision);
        GameManager.Instance.player.GainMana(GameManager.Instance.player.playerStatus.Stats[PlayerStatusType.ManaRegenerate]);
    }
    protected virtual void BreakWall()
    {
        GameObject.Destroy(this.gameObject);
    }

    protected abstract void WallReact(Collider2D collision);
}
