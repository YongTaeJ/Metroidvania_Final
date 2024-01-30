using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int _maxHp = 10;
    public int _Hp;
    public int _damage = 5;

    public Animator _animator;
    public Rigidbody2D _rigidbody;
    public PlayerInputController _controller;
    private void Awake()
    {
        _controller = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _Hp = _maxHp;
        GameManager.Instance.player = this;
    }





}
