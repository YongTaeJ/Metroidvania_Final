using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    private void Start()
    {
        PlayerDamageUp();
    }

    private void SetPlayerPosition()
    {
    }

    private void PlayerDamageUp()
    {
        GameManager.Instance.player.playerStatus.AddStat(PlayerStatusType.Damage, 10000);
    }
}
