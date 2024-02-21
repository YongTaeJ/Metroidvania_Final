using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Grid))]
public class MapGridEditor : Editor
{
    Grid grid;
    public int ObjectIndex;
    public int SelectIndex;

    public void OnEnable()
    {
        grid = (Grid)target;
    }

    void OnSceneGUI()
    {
        grid = (Grid)target;

        int crtID = GUIUtility.GetControlID(FocusType.Passive);
        Event e = Event.current;

        var mousePosition = Event.current.mousePosition * EditorGUIUtility.pixelsPerPoint;
        mousePosition.y = Camera.current.pixelHeight - mousePosition.y;
        Ray ray = Camera.current.ScreenPointToRay(mousePosition);

        if (Event.current.button == 0)
        {
            if (SelectIndex == 0)
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    GUIUtility.hotControl = crtID;
                    e.Use();
                    DrawObject(true, ray.origin);
                }
                if (Event.current.type == EventType.MouseDrag)
                {
                    DrawObject(false, ray.origin);
                }
            }
            else if (SelectIndex == 1)
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    GUIUtility.hotControl = crtID;
                    e.Use();
                    DestroyObject(ray.origin);
                }
                if (Event.current.type == EventType.MouseDrag)
                {
                    DestroyObject(ray.origin);
                }
            }
        }
    }

    private void DrawObject(bool drag, Vector3 pos)
    {
        grid = (Grid)target;
        Vector3 createPos = new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
            Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, 0f);
        if (CheckCompareObject(createPos, drag))
        {
            GameObject createObject = (GameObject)PrefabUtility.InstantiatePrefab(grid.prefabsList[ObjectIndex]);
            createObject.transform.parent = grid.transform;
            createObject.transform.position = createPos;
            Undo.RegisterCreatedObjectUndo(createObject, "create Object");
        }
    }

    private void DestroyObject(Vector3 pos)
    {
        grid = (Grid)target;
        Vector3 createPos = new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
            Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, 0f);
        if (CheckCompareObject(createPos, true))
        {

        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        grid = (Grid)target;
        EditorGUILayout.BeginHorizontal();
        string[] ObjectOptions = new string[grid.prefabsList.Length];
        string[] SelectOptions = { "그리기", "지우기" };

        for (int i = 0; i < ObjectOptions.Length; i++)
        {
            if (grid.prefabsList[i] != null)
            {
                ObjectOptions[i] = grid.prefabsList[i].name;
            }
        }

        ObjectIndex = EditorGUILayout.Popup(ObjectIndex, ObjectOptions);
        SelectIndex = EditorGUILayout.Popup(SelectIndex, SelectOptions);

        EditorGUILayout.EndHorizontal();
    }

    bool CheckCompareObject(Vector3 newPos, bool destoryObject)
    {
        Transform[] gridobjList = grid.transform.GetComponentsInChildren<Transform>();
        foreach (var obj in gridobjList)
        {
            if (obj.transform.position == newPos)
            {
                if (destoryObject)
                {
                    Undo.DestroyObjectImmediate(obj.gameObject);
                }
                return false;
            }
        }
        return true;
    }

}
#endif