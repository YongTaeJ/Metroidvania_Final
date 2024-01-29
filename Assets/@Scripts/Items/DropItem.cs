using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private ItemType _itemType;
    private int _ID;
    private int _value;

    public void Initialize(ItemType itemType, int ID, int value)
    {
        _itemType = itemType;
        _ID = ID;
        _value = value;

        PopItem();
    }

    private void PopItem()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float x = Random.Range(-45f, 45f) / 360;
        float y = 1 - x;

        rigidbody.AddForce( new Vector2(x,y) * 300);
    }

    #region Monobehaviour
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ItemManager.Instance.AddItem(_itemType, _ID, _value);
            // TODO => 소리 + 이펙트, 이펙트는 안해도 될듯
            Destroy(gameObject);
        }
    }
    #endregion
}
