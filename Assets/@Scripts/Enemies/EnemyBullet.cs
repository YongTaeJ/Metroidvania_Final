using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private float _speed;

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

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void InitDirection(Vector2 direction)
    {
        _direction = direction;

        // 계산식은 GPT한테 물어봄 !!
        float angleRadians = Mathf.Atan2(_direction.y, _direction.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        transform.Find("Sprite").eulerAngles = new Vector3(0,0, angleDegrees);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // TODO. Player에 연결된 메서드를 통해 데미지 주기
            CancelInvoke();
            Destroy(gameObject);
        }   
    }
}
