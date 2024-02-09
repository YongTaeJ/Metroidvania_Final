using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodsButton : MonoBehaviour
{
    #region variables
    private Image _image;
    private TMP_Text _nameText;
    private TMP_Text _costText;
    #endregion

    public void Initialize()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _costText = transform.Find("CostText").GetComponent<TMP_Text>();
    }
}
