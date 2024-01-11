using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorWindow : EditorWindow
{
    [MenuItem("Editor/MapEditor")]
    static public void Init()
    {
        MapEditorWindow window = GetWindow<MapEditorWindow>(typeof(Square));
        window.minSize = new Vector2(100, 100);
        window.maxSize = new Vector2(500, 500);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Box(Resources.Load<Texture2D>("Slime00"), GUILayout.Width(50), GUILayout.Height(50));

        if (GUILayout.Button("버튼"))
        {
            Debug.Log("버튼 반응");
        }
        if (GUILayout.RepeatButton("반복 버튼"))
        {
            Debug.Log("반복 버튼 반응");
        }
        if (EditorGUILayout.DropdownButton(new GUIContent("드롭 다운 버튼"), FocusType.Keyboard))
        {
            Debug.Log("드롭 다운 버튼 반응");
        }
    }
}
