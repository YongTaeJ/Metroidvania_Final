using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperBookTab : MonoBehaviour
{
    private Button _worldMapButton;
    private Button _statusButton;
    private Button _inventoryButton;
    private Button _constructButton;
    private Button _saveButton;
    private Button _settingsButton;
    // TODO => 프로퍼티로 풀어주거나 혹은 여기서 할당까지 처리

    private void Awake()
    {
        _worldMapButton = transform.Find("WorldMap").GetComponent<Button>();
        _statusButton = transform.Find("Status").GetComponent<Button>();
        _inventoryButton = transform.Find("Inventory").GetComponent<Button>();
        _constructButton = transform.Find("Construct").GetComponent<Button>();
        _saveButton = transform.Find("Save").GetComponent<Button>();
        _settingsButton = transform.Find("Settings").GetComponent<Button>();
    }
}
