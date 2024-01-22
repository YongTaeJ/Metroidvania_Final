using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerFinder : PlayerFinder
{
    protected override void Awake()
    {
        IsPlayerEnter = true;
        CurrentTransform = GameObject.Find("Player").transform;
    }
}