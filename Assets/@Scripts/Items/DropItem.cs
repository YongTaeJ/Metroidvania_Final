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
    private float attractSpeed = 5f;

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
        float angle = Random.Range(45, 135) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float forceMagnitude = Random.Range(300, 500); 
        rigidbody.AddForce(direction * forceMagnitude);
    }

    private void Vanish()
    {
        PoolManager.Instance.Push(gameObject);
    }


    #region Monobehaviour

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
        StartCoroutine(AttractRoutine(GameManager.Instance.player.transform));
    }

    private IEnumerator AttractRoutine(Transform playerTransform)
    {
        float elapsedTime = 0f; // 경과 시간
        float accelerationFactor = 100f; // 시간에 따른 가속도
        float minDistanceToCollect = 0.3f; // 아이템이 먹어질 거리


        while (Vector3.Distance(transform.position, playerTransform.position) > minDistanceToCollect)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = attractSpeed + (accelerationFactor * elapsedTime);

            Vector3 step = Vector3.MoveTowards(transform.position, playerTransform.position, currentSpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(step);

            yield return null;
        }

        CollectItem(); // 아이템이 플레이어와 충분히 가까워졌을 때 수집 처리
    }
}
