using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SwordAurorObject : MonoBehaviour
{
    private int _damage = 10;

    void Start()
    {
        StartCoroutine(DestroySwordAurorObject());
    }

    private IEnumerator DestroySwordAurorObject()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().GetDamaged(_damage, collision.transform);
        }
    }
}
