using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private ItemType _itemType;
    private int _ID;
    private int _value;
    private bool _isGrounded = false;
    public ParticleSystem itemGlowEffect;
    public void Initialize(ItemType itemType, int ID, int value)
    {
        _itemType = itemType;
        _ID = ID;
        _value = value;

        Invoke("Vanish", 30f);
        PopItem();
        ToggleGlowEffect();
    }

    private void ToggleGlowEffect()
    {
        // Equipment 또는 Skill 타입일 경우 파티클 효과를 활성화
        if (_itemType == ItemType.Equipment || _itemType == ItemType.Skill)
        {
            if (itemGlowEffect != null)
            {
                itemGlowEffect.Play();
            }
        }
        else // 그 외의 경우 파티클 효과를 비활성화
        {
            if (itemGlowEffect != null)
            {
                itemGlowEffect.Stop();
            }
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _isGrounded)
        {
            // 아이템 획득 로직 처리
            CollectItem();
        }
    }

    private void CollectItem()
    {
        if (_itemType == ItemType.Gold) PlayCoinSound();
        ItemManager.Instance.AddItem(_itemType, _ID, _value);
        CancelInvoke("Vanish");
        Vanish();
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        if(_itemType == ItemType.Gold) PlayCoinSound();
    //        ItemManager.Instance.AddItem(_itemType, _ID, _value);
    //        CancelInvoke();
    //        Vanish();
    //    }
    //}
    #endregion

    private void PlayCoinSound()
    {
        var coinSound = ResourceManager.Instance.GetAudioClip("CoinSound");
        SFX.Instance.PlayOneShot(coinSound, 0.3f);
    }
}
