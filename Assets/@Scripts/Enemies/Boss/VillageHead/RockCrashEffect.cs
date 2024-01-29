using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCrashEffect : MonoBehaviour
{
    private void Awake()
    {
        Invoke("DestroyMySelf", 0.2f);
    }

    private void DestroyMySelf()
    {
        Destroy(gameObject);
    }
}
