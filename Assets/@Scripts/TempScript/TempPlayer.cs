using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    private void Start()
    {
        Invoke("SetPlayerPosition", 0.1f);
    }

    private void SetPlayerPosition()
    {
        GameManager.Instance.player.transform.position = new Vector2(-5, -3);
    }
}
