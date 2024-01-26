using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SwordAurorObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _speed = 1000f;
    private int _damage = 10;

    //public Vector2 initialVelocity;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroySwordAurorObject());
    }

    private IEnumerator DestroySwordAurorObject()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        //_rigidbody.velocity = initialVelocity * Time.fixedDeltaTime;
        _rigidbody.velocity = Vector2.right * _speed * Time.fixedDeltaTime;
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
