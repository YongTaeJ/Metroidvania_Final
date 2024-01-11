using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Square))]
public class SquareEditor : Editor
{
    private Square _square;

    private void OnEnable()
    {
        _square = target as Square;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _square.value = EditorGUILayout.IntField(_square.value);
    }
}
