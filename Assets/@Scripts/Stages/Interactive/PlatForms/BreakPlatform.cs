using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    // 몇번 밟았을 때 없어지는 발판
    // 플레이어가 밟은 뒤에 점프하면 사라지게 작동
    [SerializeField]
    private int _standCount = 1;
    private float _recoverTime = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.transform.position.y > this.transform.position.y)
            {
                _standCount--;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D playerRigidbody = collision.collider.GetComponent<Rigidbody2D>();

        if (playerRigidbody.velocity.y > 0 && _standCount < 1)
        {
            transform.parent.gameObject.SetActive(false);
            Invoke("RecoverPlatform", _recoverTime);
        }
    }

    private void RecoverPlatform()
    {
        transform.parent.gameObject.SetActive(true);
    }
}