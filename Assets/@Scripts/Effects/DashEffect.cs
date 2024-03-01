using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    [SerializeField]
    private float _activeTime = 0.1f;
    private float _alpha;
    [SerializeField]
    private float _alphaSet = 0.8f;
    [SerializeField]
    private float _alphaDecay = 0.85f;

    private Transform player;

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _playerSpriteRenderer;

    private Color color;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

        _alpha = _alphaSet;
        _spriteRenderer.sprite = _playerSpriteRenderer.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;

        color = new Color(230f / 255f, 120f / 255f, 255f / 255f, _alpha);
        _spriteRenderer.color = color; // 초기 색상 설정
    }

    private void Update()
    {
        _alpha -= _alphaDecay * Time.deltaTime;
        color.a = _alpha;
        _spriteRenderer.color = color;

        if (_alpha <= 0.01f)
        {
            ResourceManager.Instance.Destroy(gameObject);
        }
    }
}
