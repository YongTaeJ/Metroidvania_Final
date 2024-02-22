using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUISound : MonoBehaviour
{
    AudioClip _popupSound;
    private bool _first = true;

    private void Awake()
    {
        _popupSound = ResourceManager.Instance.GetAudioClip("PopupUIEnterSound");
    }

    private void OnEnable()
    {
        if (_first)
        {
            _first = false;
            return;
        }

        SFX.Instance.PlayOneShot(_popupSound);
    }
}
