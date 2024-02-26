using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    #region variables
    private Image _image;
    private Button _button;
    private int _ID;
    #endregion

    public void Initialize(int ID)
    {
        _ID = ID;
        InitComponents();
        InitDatas(ID);
        RefreshButton();
    }

    public void InitAction(BuildingUI parents)
    {
        _button.onClick.AddListener(() => parents.BuildingInformPanel.SetBuildingInfo(_ID));
    }

    private void InitComponents()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void InitDatas(int ID)
    {
        Sprite sprite = ItemManager.Instance.GetSprite(ItemType.Building, ID);

        _image.sprite = sprite;
    }

    public void RefreshButton()
    {
        if(ItemManager.Instance.GetItemStock(ItemType.Building, _ID) == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

