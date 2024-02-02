using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, emptyHeart;
    private Image heartImege;

    private void Awake()
    {
        heartImege = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImege.sprite = emptyHeart;
                break;
            case HeartStatus.Half:
                heartImege.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                heartImege.sprite = fullHeart;
                break;
        }
    }
}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}