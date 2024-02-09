using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    #region variables
    private TMP_Text _nameText;
    private TMP_Text _YNText;
    private Image _image;
    private Button _button;
    private int _ID;
    private Action<Item> _onButtonClick;
    #endregion

    public void Initialize(int ID)
    {
        _ID = ID;
        InitComponents();
        InitDatas(ID);
    }

    public void InitAction(ConstructUI parents)
    {
        _onButtonClick = parents.InformPanel.SetInformPanel;
    }

    private void InitComponents()
    {
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _YNText = transform.Find("YNText").GetComponent<TMP_Text>();
        _image = transform.Find("Image").GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnBuildingInform);
    }

    private void InitDatas(int ID)
    {
        var SO = SOManager.Instance.GetBuildingSO(ID);
        Sprite sprite = ItemManager.Instance.GetSprite(ItemType.Building, ID);
        _nameText.text = SO.ConstructName;

        if(SO.IsBuildable())
        {
            SetYes();
        }
        else
        {
            SetNo();
        }

        _image.sprite = sprite;
    }


    private void SetYes()
    {
        _YNText.text = "건축 가능";
        _YNText.color = Color.green;
    }

    private void SetNo()
    {
        _YNText.text = "건축 불가능";
        _YNText.color = Color.red;
    }

    private void OnBuildingInform()
    {
        var item = ItemManager.Instance.GetItemDict(ItemType.Building)[_ID];
        _onButtonClick.Invoke(item);
    }

    public bool IsValidButton()
    {
        // 해당 건물이 없으면 => 지을 수 있으니까 True
        bool flag = ItemManager.Instance.HasItem(ItemType.Building, _ID);
        return !flag;
    }
}
