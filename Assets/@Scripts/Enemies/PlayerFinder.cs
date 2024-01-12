using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public bool IsPlayerEnter { get; private set; }
    public Transform CurrentTransform { get; private set; }

    private void Awake()
    {
        IsPlayerEnter = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            IsPlayerEnter = true;
            CurrentTransform = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            IsPlayerEnter = false;
            CurrentTransform = null;
        }
    }
}
