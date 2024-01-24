using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    private void StartBossBattle()
    {
        // 보스전에 필요한 여러 프리펩들을 가지고 있다.
        // 1. 플레이어 정지
        // 2. 퇴로가 닫힘.
        // 3. 카메라 이동 -> 보스룸 가운데로
        // 4. 촌장 날아오는 애니메이션
        // 5. 대화창, Taunt 모션
        // 6. 전투 시작
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartBossBattle();
            Destroy(gameObject);
        }
    }
}
