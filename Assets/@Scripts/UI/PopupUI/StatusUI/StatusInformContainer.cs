using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StatusInformContainer : MonoBehaviour
{
    private StatusInformPanel _informPanels;

    private void Awake()
    {
        _informPanels = transform.Find("ItemInform").GetComponent<StatusInformPanel>();
    }

    public void SetItemInform(Item item)
    {
        _informPanels.SetInform(item);
    }
}
