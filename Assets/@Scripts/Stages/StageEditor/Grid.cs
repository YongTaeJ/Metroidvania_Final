using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [HideInInspector]
    public float width = 1f;
    [HideInInspector]
    public float height = 1f;
    [HideInInspector]
    public Color color = Color.white;

    public GameObject[] prefabsList;
}
