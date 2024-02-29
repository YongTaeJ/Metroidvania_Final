using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerFinder : PlayerFinder
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        IsPlayerEnter = true;
        CurrentTransform = GameObject.Find("Player").transform;
    }
}
