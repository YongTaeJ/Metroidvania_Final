using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public StatusInformContainer InformContainer { get; private set;}
    public StatusItemContainer ItemContainer { get; private set;}
    public StatusNonItemContainer NonItemContainer {get; private set;}
    private void Awake()
    {
        InformContainer = GetComponentInChildren<StatusInformContainer>();
        ItemContainer = GetComponentInChildren<StatusItemContainer>();
        NonItemContainer = GetComponentInChildren<StatusNonItemContainer>();

        ItemContainer.Initialize(this);

        Button button = transform.Find("ExitButton").GetComponent<Button>();
        button.onClick.AddListener( () => { gameObject.SetActive(false);});
    }

    private void OnEnable()
    {
        ItemContainer.CheckItems();
    }
}
