using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private ItemType _itemType;
    private int _ID;
    private int _value;
    private bool _dropstate = false;
    public ParticleSystem itemGlowEffect;
    public void Initialize(ItemType itemType, int ID, int value)
    {
        _itemType = itemType;
        _ID = ID;
        _value = value;
        _dropstate = false;
        Invoke("Vanish", 30f);
        PopItem();
        ToggleGlowEffect();
        StartCoroutine(DropDelay());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _dropstate)
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

    #endregion

    private void PlayCoinSound()
    {
        var coinSound = ResourceManager.Instance.GetAudioClip("CoinSound");
        SFX.Instance.PlayOneShot(coinSound, 0.3f);
    }

    private IEnumerator DropDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _dropstate = true;
    }
}
