using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstrcutIndicator : MonoBehaviour
{
    private Image _itemImage;
    private TMP_Text _originStock;
    private TMP_Text _resultStock;

    public void Initialize()
    {
        _itemImage = transform.Find("ItemImage").GetComponent<Image>();
        _originStock = transform.Find("OriginStock").GetComponent<TMP_Text>();
        _resultStock = transform.Find("ResultStock").GetComponent<TMP_Text>();
    }

    // 계산은 Container에서 처리
    public void SetValue(Sprite sprite, int origin, int result)
    {
        _itemImage.sprite = sprite;
        _originStock.text = origin.ToString();
        _resultStock.text = result.ToString();
    }

    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
