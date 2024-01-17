using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    #region Properties
    public bool IsPlayerEnter { get; private set; }
    public Transform CurrentTransform { get; private set; }
    #endregion

    #region Monobehaviour
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
        }
    }
    #endregion
}
