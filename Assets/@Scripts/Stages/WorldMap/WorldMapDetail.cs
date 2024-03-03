using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapDetail : MonoBehaviour
{
    private int _selectedButtonIndex;
    private GameObject _worldMapImage;

    [SerializeField] private Button[] _detailMapButton;
    [SerializeField] private Image[] _detailMapImage;

    private void Awake()
    {
        _worldMapImage = transform.Find("WorldMapImage").gameObject;

        for (int i = 0; i < _detailMapButton.Length; i++)
        {
            int index = i;
            _detailMapButton[i].onClick.AddListener(() => ClickDetailMap(index));
            _detailMapButton[i].gameObject.SetActive(false);
        }
        UpdateDetailMapButton();
    }

    public void ClickDetailMap(int index)
    {
        CloseDetailMapImage();
        _selectedButtonIndex = index;
        _detailMapImage[index].gameObject.SetActive(true);
        _worldMapImage.SetActive(false);
        for (int i = 0; i < _detailMapButton.Length; i++)
        {
            _detailMapButton[i].gameObject.SetActive(false);
        }
    }

    public void CloseDetailMapImage()
    {
        foreach (Image img in _detailMapImage)
        {
            img.gameObject.SetActive(false);
        }

        if (_worldMapImage != null)
        {
            _worldMapImage.SetActive(true);
        }
    }

    public void UpdateDetailMapButton()
    {
        int i = ItemManager.Instance.GetItemStock(ItemType.Map, 0);
        for (int j = 0; j < i; j++)
        {
            _detailMapButton[j].gameObject.SetActive(true);
        }
    }
}
