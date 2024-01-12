using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class MapEditorWindow : EditorWindow
{
    private static Grid grid;

    [MenuItem("Editor/MapEditor")]
    static public void Init()
    {
        MapEditorWindow window = GetWindow<MapEditorWindow>(typeof(Grid));
        window.minSize = new Vector2(415, 200);
        window.maxSize = new Vector2(415, 800);
        window.Show();
    }

    private void Awake()
    {
        if (grid == null)
        {
            grid = FindObjectOfType<Grid>();
            grid.gameObject.SetActive(true);
        }
        else
        {
            grid.gameObject.SetActive(true);
        }
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

        //GUILayout.BeginArea(new Rect(0, 0, 100, 100), new GUIStyle("Box")); // 버튼도 가능
        //GUILayout.Button("아이콘");
        //GUILayout.EndArea();

        GUILayout.Label("Monster Random Create", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Slime"))
        {
            CreateSlime();
        }
        if (GUILayout.Button("Create Plant"))
        {
            CreatePlant();
        }
        GUILayout.EndHorizontal();


        GUILayout.Label("MapEditor2", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Box(Resources.Load<Texture2D>("Editor/102GreenSlime"), GUILayout.Width(200), GUILayout.Height(200));
        GUILayout.EndHorizontal();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    private void CreatePlant()
    {
        CreateObject(new ObjectCreationDatas
        {
            BaseName = "PlantBase",
            ResourceName = "Editor/Plant",
            Width = 30,
            Height = 1,
            Scale = new Vector3(0.5f, 0.5f, 1f),
            StartPosition = new Vector3(-8.5f, -0.2f, 0),
        });
    }

    private void CreateSlime()
    {
        CreateObject(new ObjectCreationDatas
        {
            BaseName = "SlimeBase",
            ResourceName = "Editor/102GreenSlime",
            Width = 30,
            Height = 1,
            Scale = new Vector3(0.25f, 0.25f, 1f),
            StartPosition = new Vector3(-8.5f, 0.5f, 0),
        });
    }

    private void CreateDefaultSetting()
    {
        CreateObject(new ObjectCreationDatas());
    }

    private GameObject CreateBaseObject(string baseName)
    {
        GameObject baseObj = GameObject.Find(baseName);
        if (baseObj == null)
        {
            baseObj = new GameObject(baseName);
        }
        return baseObj;
    }

    private void CreateObject(ObjectCreationDatas datas)
    {
        Debug.Log("Create " + datas.BaseName);

        GameObject baseObj = CreateBaseObject(datas.BaseName);
        baseObj.transform.SetParent(grid.transform);

        for (int i = 0; i < datas.Width; i++)
        {
            for (int j = 0; j < datas.Height; j++)
            {
                Vector3 position = new Vector3(datas.StartPosition.x + i, datas.StartPosition.y + j, datas.StartPosition.z);

                if (Physics2D.OverlapPoint(position) == null)
                {
                    if (UnityEngine.Random.Range(0f, 1f) <= 0.25f)
                    {
                        GameObject childObj = new GameObject(string.Format("Child-{0}-{1}", i, j));
                        SpriteRenderer sr = childObj.AddComponent<SpriteRenderer>();
                        sr.sprite = Resources.Load(datas.ResourceName, typeof(Sprite)) as Sprite;
                        sr.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));

                        childObj.AddComponent<BoxCollider2D>();

                        childObj.transform.position = new Vector3(datas.StartPosition.x + i, datas.StartPosition.y + j, datas.StartPosition.z);
                        childObj.transform.SetParent(baseObj.transform);
                        childObj.transform.localScale = datas.Scale;
                    }
                }
            }
        }

        Undo.RegisterCreatedObjectUndo(baseObj, "Create Custom Object");
        Selection.activeObject = baseObj;
    }

    public struct ObjectCreationDatas
    {
        public string BaseName { get; set; }
        public string ResourceName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector3 Scale { get; set; }
        public Vector3 StartPosition { get; set; }

        public ObjectCreationDatas(
            string baseName = "DefaultBase",
            string resourceName = "DefaultResource",
            int width = 10,
            int height = 10,
            Vector3 scale = default,
            Vector3 startPosition = default)
        {
            BaseName = baseName;
            ResourceName = resourceName;
            Width = width;
            Height = height;
            Scale = scale != default ? scale : new Vector3(1, 1, 1);
            StartPosition = startPosition;
        }
    }
}

