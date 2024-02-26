using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Teleport : MonoBehaviour
{
    private TextMeshProUGUI _teleportText;
    [SerializeField]
    private Vector3 _teleportPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = _teleportPosition;
            SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("Warp"));            
            OpenTrapText();
        }
    }
    private void OpenTrapText()
    {
        _teleportText = UIManager.Instance.GetUI(PopupType.ToolTip).GetComponentInChildren<TextMeshProUGUI>();
        _teleportText.text = "Teleported";
        UIManager.Instance.OpenPopupUI(PopupType.ToolTip);
        StartCoroutine(DeactiveText(2));
    }

    private IEnumerator DeactiveText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UIManager.Instance.ClosePopupUI(PopupType.ToolTip);
    }
}
