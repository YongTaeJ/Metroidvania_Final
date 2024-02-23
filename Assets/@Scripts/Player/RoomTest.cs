using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTest : MonoBehaviour
{
    private Collider2D roomCollider;

    private void Awake()
    {
        roomCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("호출");
            // 플레이어가 방에 진입하면 해당 방의 Collider2D를 Cinemachine Confiner의 Bounding Shape로 설정
            //CameraManager.Instance.ChangeBoundingShape(roomCollider);
        }
    }
}
