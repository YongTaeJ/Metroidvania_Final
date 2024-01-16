using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    [SerializeField]
    private int _standCount = 1;

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
        }
    }
}
