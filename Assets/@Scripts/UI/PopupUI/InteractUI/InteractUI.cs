using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    private TMP_Text _text;
    private Image _image;
    private bool _isInitialized = false;

    private void Awake()
    {
        _text = transform.Find("Text").GetComponent<TMP_Text>();
        _image = transform.Find("Image").GetComponent<Image>();
        _isInitialized = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if(!_isInitialized) return;
        float time = 0.5f;

        _text.alpha = 0;
        _text.DOFade(1, time);
        _image.color = new Color (1,1,1,0);
        _image.DOFade(1, time);
    }
}
