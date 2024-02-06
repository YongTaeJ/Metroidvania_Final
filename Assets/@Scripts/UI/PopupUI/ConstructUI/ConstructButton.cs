using UnityEngine;
using UnityEngine.UI;

public class ConstructButton : MonoBehaviour
{
    // TODO => 임시 코드. 전체적인 개선 필요!!

    #region variables
    private Button _button;
    #endregion

    public void Initialize()
    {
        _button = GetComponent<Button>();
    }

    public void Disable()
    {
        _button.enabled = false;
    }

    public void Enable()
    {
        _button.enabled = true;
    }
}
