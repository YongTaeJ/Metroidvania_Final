using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.player.playerStatus.AddStat(PlayerStatusType.Damage, 100);
    }
}
