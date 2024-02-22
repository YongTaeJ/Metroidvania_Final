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

        Invoke("Vanish", 30f);
        PopItem();
    }

    private void PopItem()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float x = Random.Range(-45f, 45f) / 360;
        float y = 1 - x;

        rigidbody.AddForce( new Vector2(x,y) * 300);
    }

    private void Vanish()
    {
        PoolManager.Instance.Push(gameObject);
    }
    

    #region Monobehaviour
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(_itemType == ItemType.Gold) PlayCoinSound();
            ItemManager.Instance.AddItem(_itemType, _ID, _value);
            CancelInvoke();
            Vanish();
        }
    }
    #endregion

    private void PlayCoinSound()
    {
        var coinSound = ResourceManager.Instance.GetAudioClip("CoinSound");
        SFX.Instance.PlayOneShot(coinSound, 0.3f);
    }
}
