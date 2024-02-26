using UnityEngine;
using UnityEngine.UI;

public class UIClickSound : MonoBehaviour
{
    // 버튼 컴포넌트를 찾아 클릭사운드를 연결해준 뒤 파괴됩니다.
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => SFX.Instance.PlayOneShot("UIClickSound") );
        Destroy(this);
    }
}
