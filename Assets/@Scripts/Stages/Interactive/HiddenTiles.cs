using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class HiddenTiles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TilemapRenderer tilemapRenderer = GetComponent<TilemapRenderer>();
            tilemapRenderer.material.DOFade(0f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TilemapRenderer tilemapRenderer = GetComponent<TilemapRenderer>();
            tilemapRenderer.material.DOFade(1f, 1f);
        }
    }
}
