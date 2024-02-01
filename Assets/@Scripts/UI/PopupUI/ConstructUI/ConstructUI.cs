using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructUI : MonoBehaviour
{
    private void Awake() 
    {
        Button button = transform.Find("ExitButton").GetComponent<Button>();
        button.onClick.AddListener( () => { gameObject.SetActive(false);});
    }
}
