using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverlaySigns : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private TextMeshProUGUI _signText;

    public string SignId;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            string message = SignDataManager.Instance.GetMessage(SignId);
            _signText.text = message;
            _panel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _panel.SetActive(false);
            _signText.text = "";
        }
    }
}
