using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Coin,
    Brick,
    Wood
}

public class Item : MonoBehaviour
{
    #region Fields
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _value;
    #endregion

    #region Properties
    public ItemType ItemType => _itemType;
    public int Value => _value;
    #endregion

    #region Monobehaviour
    private void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float x = Random.Range(-45f, 45f) / 360;
        float y = 1 - x;

        rigidbody.AddForce( new Vector2(x,y) * 300);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // ItemType을 토대로 인벤토리 등 필요한 곳에 정보 전달
            Destroy(gameObject);
        }
    }
    #endregion
}
