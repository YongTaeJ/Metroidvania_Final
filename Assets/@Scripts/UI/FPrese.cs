using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FPrese : MonoBehaviour
{
    public float maxDistance = 10f;
    private GameObject player;

    void Start()
    {
        player = GameManager.Instance.player.gameObject;
    }

    void Update()
    {
        // 플레이어 오브젝트가 존재하는지 확인
        if (player != null)
        {
            // 플레이어와의 현재 거리를 계산
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // 설정한 최대 거리를 초과하면 오브젝트 파괴
            if (distance > maxDistance)
            {
                ResourceManager.Instance.Destroy(this.gameObject);
            }
        }
    }
}
