using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Coin1,
    Coin10,
    Coin100,
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    public ItemType ItemType => _itemType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // ItemType을 토대로 인벤토리 등 필요한 곳에 정보 전달
            Destroy(gameObject);
        }
    }
}
