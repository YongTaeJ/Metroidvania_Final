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

    public void Initialize()
    {
        InitComponents();
        InitDatas();
        InitButton();
    }

    private void InitComponents()
    {
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _YNText = transform.Find("YNText").GetComponent<TMP_Text>();
        _image = transform.Find("Image").GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void InitDatas()
    {
        _nameText.text = "asdf";
        _YNText.text = "asdf";
        // _image.sprite = sprite;
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
