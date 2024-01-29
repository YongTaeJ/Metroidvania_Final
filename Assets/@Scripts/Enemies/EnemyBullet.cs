using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    #region Fields
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private float _speed;
    private int _damage;
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _speed = 10f;
        _direction = Vector2.up;
        Invoke("DestroySelf", 5f);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().GetDamaged(_damage, this.transform);
            CancelInvoke();
            Destroy(gameObject);
        }
        if(other.CompareTag("Ground"))
        {
            CancelInvoke();
            Destroy(gameObject);
        }
    }
    #endregion

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void Initialize(Vector2 direction, int damage)
    {
        _direction = direction;
        _damage = damage;

        // 계산식은 GPT한테 물어봄 !!
        float angleRadians = Mathf.Atan2(_direction.y, _direction.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        transform.Find("Sprite").eulerAngles = new Vector3(0,0, angleDegrees);
    }
}
