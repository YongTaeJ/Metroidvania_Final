using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructInformPanel : MonoBehaviour
{
    #region variables
    private Image _buildingImage;
    private TMP_Text _nameText;
    private TMP_Text _conditionText;
    private TMP_Text _descriptionText;
    private TMP_Text _materialText;
    private ConstructButton _constructButton;
    #endregion
    public void Initialize()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _buildingImage = transform.Find("BuildingImage").GetComponent<Image>();
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _conditionText = transform.Find("ConditionText").GetComponent<TMP_Text>();
        _descriptionText = transform.Find("DescriptionText").GetComponent<TMP_Text>();
        _materialText = transform.Find("MaterialText").GetComponent<TMP_Text>();
        _constructButton = transform.Find("ConstructButton").GetComponent<ConstructButton>();

        _constructButton.Initialize();
    }

    public void SetInformPanel()
    {

    }
}
