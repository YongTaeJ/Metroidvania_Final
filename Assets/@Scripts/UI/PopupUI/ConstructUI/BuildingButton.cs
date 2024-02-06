using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    #region variables
    private TMP_Text _nameText;
    private TMP_Text _YNText;
    private Image _image;
    private Button _button;
    #endregion

    public void Initialize(int ID)
    {
        InitComponents();
        InitDatas(ID);
        InitButton();
    }

    private void InitComponents()
    {
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _YNText = transform.Find("YNText").GetComponent<TMP_Text>();
        _image = transform.Find("Image").GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void InitDatas(int ID)
    {
        var SO = BuildingSOManager.Instance.GetSO(ID);
        Sprite sprite = ItemManager.Instance.GetSprite(ItemType.Building, ID);
        _nameText.text = SO.ConstructName;

        if(IsBuildable(SO))
        {
            SetYes();
        }
        else
        {
            SetNo();
        }

        _image.sprite = sprite;
    }

    private bool IsBuildable(BuildingSO SO)
    {
        return true;
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

    private void InitButton()
    {
        // TODO => 매개변수로 메서드를 받을 수도.
        _button.onClick.AddListener(OnBuildingInform);
    }

    private void OnBuildingInform()
    {
        // TODO => BuildingInform에 정보 전달
    }

    public bool IsValidButton()
    {
        // TODO => It is constructed already?
        return true;
    }
}
