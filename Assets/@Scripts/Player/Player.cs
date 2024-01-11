using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public Collider2D Collider { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider2D>();
    }
}
