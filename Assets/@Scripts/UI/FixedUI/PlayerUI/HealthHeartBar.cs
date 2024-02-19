using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    private GameObject heartPrefab;
    List<PlayerHeart> hearts = new List<PlayerHeart>();
    private Transform healthUI;
    private void Awake()
    {
        heartPrefab = Resources.Load<GameObject>("UI/Heart");
        healthUI = transform.Find("HealthUI");
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            //GameManager.Instance.player.OnHealthChanged += DrawHearts;
            DrawHearts();
        }
    }

    public void DrawHearts()
    {
        ClearHearts();

        float maxHealthRemainder = GameManager.Instance.player._maxHp % 2;
        int heartsToMake = (int)((GameManager.Instance.player._maxHp / 2) + maxHealthRemainder);
        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0;i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(GameManager.Instance.player._hp - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = PoolManager.Instance.Pop(heartPrefab);
        newHeart.transform.SetParent(transform);

        PlayerHeart heartComponent = newHeart.GetComponent<PlayerHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts =  new List<PlayerHeart> ();
    }
}
