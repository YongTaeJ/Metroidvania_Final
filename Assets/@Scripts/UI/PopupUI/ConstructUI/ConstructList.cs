using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConstructList : MonoBehaviour
{
    private GameObject _buildingButton;
    private Transform _contentContainer;
    private List<BuildButton> _buildingButtons;

    public void Initialize(ConstructUI parents)
    {
        _buildingButtons = new List<BuildButton>();
        _buildingButton = Resources.Load<GameObject>("UI/BuildButton");
        _contentContainer = transform.Find("Viewport/Content");

        var buildings = ItemManager.Instance.GetItemDict(ItemType.Building);
        foreach(Item building in buildings.Values)
        {
            if(building.Stock == 0)
            {
                var buildingButton = Instantiate(_buildingButton, _contentContainer).GetComponent<BuildButton>();
                buildingButton.Initialize(building.ItemData.ID);
                buildingButton.InitAction(parents);
                _buildingButtons.Add(buildingButton);
            }
        }

        RefreshValidButtons();
    }

    private void OnEnable()
    {
        if(_buildingButtons == null) return;

        RefreshValidButtons();
    }

    public void RefreshValidButtons()
    {
        for(int i= _buildingButtons.Count-1 ; i >= 0 ; i--)
        {
            if(!_buildingButtons[i].IsValidButton())
            {
                Destroy(_buildingButtons[i].gameObject);
                _buildingButtons.RemoveAt(i);
            }
        }
    }
}
