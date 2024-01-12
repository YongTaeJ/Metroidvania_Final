using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class MapEditorWindow : EditorWindow
{

    [MenuItem("Editor/MapEditor")]
    static public void Init()
    {
        MapEditorWindow window = GetWindow<MapEditorWindow>(typeof(Square));
        window.minSize = new Vector2(415, 200);
        window.maxSize = new Vector2(415, 800);
        window.Show();
    }



    private void OnGUI()
    {
        GUILayout.Label("MapEditor", EditorStyles.boldLabel);


        GUILayout.BeginHorizontal();
        if (GUILayout.Button(Resources.Load<Texture2D>("Editor/001Player"), GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log("플레이어 버튼");
        }
        if (GUILayout.Button(Resources.Load<Texture2D>("Editor/101Demon"), GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log("데몬 버튼");
        }
        if (GUILayout.Button(Resources.Load<Texture2D>("Editor/102GreenSlime"), GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log("그린 슬라임 버튼");
        }
        if (GUILayout.Button(Resources.Load<Texture2D>("Editor/103YellowSlime"), GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log("옐로우 슬라임 버튼");
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(Resources.Load<Texture2D>("Editor/001Player"), GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log("플레이어 버튼");
        }
        if (GUILayout.Button(Resources.Load<Texture2D>("Editor/101Demon"), GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log("데몬 버튼");
        }
        if (GUILayout.Button(Resources.Load<Texture2D>("Editor/102GreenSlime"), GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log("그린 슬라임 버튼");
        }
        if (GUILayout.Button(Resources.Load<Texture2D>("Editor/103YellowSlime"), GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log("옐로우 슬라임 버튼");
        }
        GUILayout.EndHorizontal();


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

        //GUILayout.BeginArea(new Rect(0, 0, 100, 100), new GUIStyle("Box")); // 버튼도 가능
        //GUILayout.Button("아이콘");
        //GUILayout.EndArea();

        if (GUILayout.Button("Slime Create"))
        {
            CreateSlime();
        }


        GUILayout.Label("MapEditor2", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Box(Resources.Load<Texture2D>("Editor/102GreenSlime"), GUILayout.Width(200), GUILayout.Height(200));
        GUILayout.EndHorizontal();
    }

    private void CreateSlime()
    {
        Debug.Log("Create Slime");

        GameObject baseObj = new GameObject("Base");

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject childObj = new GameObject(string.Format("Child-{0}-{1}", i, j));
                SpriteRenderer sr = childObj.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load("Editor/102GreenSlime", typeof(Sprite)) as Sprite;
                sr.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
                childObj.transform.position = new Vector3(i, j, 0);
                childObj.transform.SetParent(baseObj.transform);
                childObj.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
        }

        Undo.RegisterCreatedObjectUndo(baseObj, "Create Custom Object");
        Selection.activeObject = baseObj;
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}

public class MapData
{
    // 여기에 맵 데이터 구조 정의 (예: 타일 배열, 객체 리스트 등)
}

// 맵 데이터를 렌더링하는 클래스
public class MapRenderer
{
    // 맵 데이터를 기반으로 맵을 렌더링하는 로직
}
