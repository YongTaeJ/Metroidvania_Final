using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class InteractUI : MonoBehaviour
{
    private TMP_Text _text;
    private bool _isInitialized = false;

    private void Awake()
    {
        _text = transform.Find("Text").GetComponent<TMP_Text>();
        _isInitialized = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if(!_isInitialized) return;

        _text.alpha = 0;
        _text.DOFade(1, 1f);
    }
}
