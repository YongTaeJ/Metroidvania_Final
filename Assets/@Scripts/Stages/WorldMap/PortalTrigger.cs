using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private Canvas _buyPortal;
    [SerializeField] private int _portalIndex;
    [SerializeField] private int _portalPrice = 50;

    private void Awake()
    {
        _buyPortal = MapManager.Instance.BuyPortal;
        Animator animator = GetComponent<Animator>();

        if (animator != null )
        {
            if (ItemManager.Instance.HasItem(ItemType.Portal, _portalIndex))
            {
                animator.SetBool("IsActivate", true);
            }
        }
    }

    public void PopupBuyPortal()
    {
        _buyPortal.gameObject.SetActive(true);
    }
}
