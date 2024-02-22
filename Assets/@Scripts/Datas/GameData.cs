using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public List<InternalItemData> Inventory;
    // 플레이어 위치
    // Vector3는 기본적으로 Serializable 속성을 가지고 있지 않으므로 직렬화가 가능하게 각각의 좌표를 저장
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    // 랜덤 정수 저장
    public int randomUniqueNumber;
}
