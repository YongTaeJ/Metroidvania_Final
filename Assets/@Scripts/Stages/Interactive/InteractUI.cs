using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactUI;

    private void Awake()
    {
        _interactUI.SetActive(false);
    }
}
