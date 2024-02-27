using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformPanel : MonoBehaviour
{
    private Image _buildingImage;
    private TMP_Text _nameText;
    private TMP_Text _descriptionText;
    private TMP_Text _effectText;
    private bool _isInitialized = false;

    private void Awake()
    {
        _buildingImage = transform.Find("BuildingImage").GetComponent<Image>();
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _descriptionText = transform.Find("DescriptionText").GetComponent<TMP_Text>();
        _effectText = transform.Find("EffectText").GetComponent<TMP_Text>();
        _isInitialized = true;
    }

    public void SetBuildingInfo(int ID)
    {
        Sprite sprite = ItemManager.Instance.GetSprite(ItemType.Building, ID);
        ItemData itemData = ItemManager.Instance.GetItemData(ItemType.Building, ID);
        string effect = SOManager.Instance.GetBuildingSO(ID).EffectDescription;

        _buildingImage.gameObject.SetActive(true);
        _buildingImage.sprite = sprite;
        _descriptionText.text = itemData.Description;
        _nameText.text = itemData.NameKor;
        _effectText.text = effect;
    }

    private void OnEnable()
    {
        if(!_isInitialized) return;

        _nameText.text = "";
        _descriptionText.text = "";
        _buildingImage.gameObject.SetActive(false);
        _descriptionText.text = "";
        _effectText.text = "";
    }
}
