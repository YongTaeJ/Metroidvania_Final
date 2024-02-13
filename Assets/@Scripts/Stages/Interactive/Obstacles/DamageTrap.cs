using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    [SerializeField] private int _trapDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamagable>().GetDamaged(_trapDamage, transform);
        }
    }
}
