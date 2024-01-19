﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public float width = 1f;
    public float height = 1f;

    public Color color = Color.white;

    public void OnDrawGizmos()
    {
        Vector3 pos = Camera.current.transform.position;
        Gizmos.color = color;

        if (width <= 0 || height <= 0)
        {
            return;
        }

        for (float y = pos.y - 540f; y < pos.y + 540f; y += height)
        {
            Gizmos.DrawLine(new Vector3(5000, Mathf.Floor(y / height) * height, 0f),
                new Vector3(-5000, Mathf.Floor(y / height) * height, 0f));
        }

        for (float x = pos.x - 540f; x < pos.x + 540f; x += width)
        {
            Gizmos.DrawLine(new Vector3(Mathf.Floor(x / width) * width, 5000, 0f),
                new Vector3(Mathf.Floor(x / width) * width, -5000, 0f));

        }
    }

    public static void ToggleGizmos(GameObject gizmos)
    {
        if (gizmos != null)
        {
            gizmos.SetActive(!gizmos.activeSelf);
        }
    }
}