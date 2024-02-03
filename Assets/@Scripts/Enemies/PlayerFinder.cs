using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    #region Properties
    public bool IsPlayerEnter { get; protected set; }
    public Transform CurrentTransform { get; protected set; }
    #endregion

    #region Monobehaviour
    protected virtual void Awake()
    {
        IsPlayerEnter = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            IsPlayerEnter = true;
            CurrentTransform = other.transform;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            IsPlayerEnter = false;
        }
    }
    #endregion
}
