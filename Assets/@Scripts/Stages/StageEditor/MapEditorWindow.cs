using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class MapEditorWindow : EditorWindow
{
    private static Grid grid;
    private GameObject gizmos;

    [MenuItem("Editor/MapEditor", false, 2)]
    static public void Init()
    {
        MapEditorWindow window = GetWindow<MapEditorWindow>(typeof(Grid));
        window.minSize = new Vector2(415, 200);
        window.maxSize = new Vector2(415, 800);
        window.Show();
    }

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        Transform gizmosTransform = grid.transform.Find("Gizmos");
        gizmos = gizmosTransform.gameObject;
    }


    private void OnGUI()
    {
        if (GUILayout.Button("Show Gizmos", GUILayout.Width(120), GUILayout.Height(30)))
        {
            DrawGizmos.ToggleGizmos(gizmos);
        }

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

        //GUILayout.BeginArea(new Rect(0, 0, 100, 100), new GUIStyle("Box")); // 버튼도 가능
        //GUILayout.Button("아이콘");
        //GUILayout.EndArea();
    }
}
#endif
