using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBGMChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BGM.Instance.Stop();
            if (collision.transform.position.x > transform.position.x)
            {
                // 오른쪽에서 충돌: 1스테이지 BGM 재생
                BGM.Instance.Play("Stage1", true);
            }
            else
            {
                // 왼쪽에서 충돌: 2스테이지 BGM 재생
                BGM.Instance.Play("Stage2", true);
            }
           
        }
    }
}
