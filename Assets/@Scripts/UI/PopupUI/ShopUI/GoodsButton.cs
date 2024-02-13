
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodsButton : MonoBehaviour
{
    #region variables
    private InternalItemData _goodsData;
    private BuyPopup _buyPopup;
    private Image _image;
    private TMP_Text _nameText;
    private TMP_Text _costText;
    private Button _button;
    #endregion

    private void Awake()
    {
        InitComponents();
    }

    public void Initialize(BuyPopup popup)
    {
        _buyPopup = popup;
    }

    public void SetGoodsData(InternalItemData data)
    {
        gameObject.SetActive(true);
        _goodsData = data;
        _image.sprite = ItemManager.Instance.GetSprite(data.ItemType, data.ID);
        _nameText.text = ItemManager.Instance.GetItemData(data.ItemType, data.ID).Name;
        _costText.text = "가격 : " + data.Stock.ToString() + " Gold";
    }

    private void InitComponents()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _costText = transform.Find("CostText").GetComponent<TMP_Text>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        _buyPopup.SetPopupData(_goodsData);
    }
}
