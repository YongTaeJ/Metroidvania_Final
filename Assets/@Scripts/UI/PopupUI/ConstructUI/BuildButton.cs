using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
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
        var data = ItemManager.Instance.GetItemData(ItemType.Building, ID);
        Sprite sprite = ItemManager.Instance.GetSprite(ItemType.Building, ID);
        _nameText.text = data.NameKor;
        SetBuildable();
        _image.sprite = sprite;
    }
    
    private void SetBuildable()
    {
        var SO = SOManager.Instance.GetBuildingSO(_ID);
        if(SO.IsBuildable())
        {
            SetYes();
        }
        else
        {
            SetNo();
        }
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
        SetBuildable();
        bool flag = ItemManager.Instance.HasItem(ItemType.Building, _ID);
        return !flag;
    }
}
