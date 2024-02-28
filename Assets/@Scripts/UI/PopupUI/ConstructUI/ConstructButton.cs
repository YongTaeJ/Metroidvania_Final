using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructButton : MonoBehaviour
{
    #region variables
    private int _currentID;
    private Button _button;
    private ConstructYNPanel _YNPanel;
    private TMP_Text _text;
    #endregion

    public void Initialize(ConstructUI constructUI)
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TMP_Text>();

        _YNPanel = constructUI.YNPanel;
        _button.onClick.AddListener(OnPopupYN);
    }

    public void Disable()
    {
        _text.color = Color.red;
        _text.text = "재료 부족";
        _button.enabled = false;
    }

    public void Enable(int ID)
    {
        _text.color = Color.black;
        _text.text = "재건 시작";
        _currentID = ID;
        _button.enabled = true;
    }

    private void OnPopupYN()
    {
        _YNPanel.OpenPanel(_currentID);
    }
}
