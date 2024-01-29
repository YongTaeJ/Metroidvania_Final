using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldSigns : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject _canvas;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canvas.SetActive(false);
        }
    }
}
