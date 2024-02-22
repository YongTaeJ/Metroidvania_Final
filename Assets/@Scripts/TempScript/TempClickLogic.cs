using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TempClickLogic : MonoBehaviour
{
    // 버튼에 일일히 속성을 붙이는 방식에서 전환
    // 문제점 : 클릭 다음 프레임에 버튼을 가려버리는 경우 먹통이 된다...
    private GraphicRaycaster _rayCaster;
    private EventSystem _eventSystem;
    private AudioClip _clickSound;

    private void Awake()
    {
        _rayCaster = GetComponent<GraphicRaycaster>();
        _eventSystem = FindObjectOfType<EventSystem>();
        _clickSound = ResourceManager.Instance.GetAudioClip("UIClickSound");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsButton())
            {
                PlayClickSound();
            }
        }
    }

    private bool IsButton()
    {
        PointerEventData data = new PointerEventData(_eventSystem);
        data.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        _rayCaster.Raycast(data, results);

        foreach(RaycastResult result in results)
        {
            Debug.Log("count.");
            if (result.gameObject.GetComponent<Button>() != null)
            {
                return true;
            }
        }

        return false;
    }

    private void PlayClickSound()
    {
        SFX.Instance.PlayOneShot(_clickSound);
    }
}
