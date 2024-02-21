using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class CreatureEditor : EditorWindow
{
    private static Grid grid;
    private GameObject gizmos;

    [MenuItem("Editor/CreatureEditor", false, 3)]
    static public void Init()
    {
        CreatureEditor window = GetWindow<CreatureEditor>(typeof(Grid));
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

        GUILayout.Label("Creature Editor", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Box(Resources.Load<Texture2D>("Editor/102GreenSlime"), GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.EndHorizontal();
        GUILayout.Label("GreenSlime");

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
            Height = 5,
            Scale = new Vector3(0.25f, 0.25f, 1f),
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
#endif