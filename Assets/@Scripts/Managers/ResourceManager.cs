using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, GameObject> prefaps = new();
    private Dictionary<string, Sprite> sprites = new();
    private Dictionary<string, AudioClip> audios = new();

    public override bool Initialize()
    {
        LoadPrefabs("Prefabs", prefaps);
        LoadAudioClip("AudioClip", audios);
        //LoadSprites("")
        // 스프라이트 현재 경로가 애매해서 보류
        return base.Initialize();
    }

    #region Prefab

    /// <summary>
    /// 지정된 경로 안에 모든 프리팹들 로드
    /// </summary>
    /// <param name="path">폴더 경로</param>
    /// <param name="prefabs">로드할 프리팹 값</param>
    private void LoadPrefabs(string path, Dictionary<string, GameObject> prefabs)
    {
        GameObject[] objs = Resources.LoadAll<GameObject>(path);
        foreach (GameObject obj in objs)
        {
            prefabs[obj.name] = obj;
        }
    }

    /// <summary>
    /// string key를 기반으로 오브젝트 가져오기
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public GameObject GetObject(string prefabName)
    {
        if (!prefaps.TryGetValue(prefabName, out GameObject prefab)) return null;
        return prefab;
    }

    #endregion

    #region Sprite

    /// <summary>
    /// 지정된 경로 안에 모든 스프라이트 로드
    /// </summary>
    /// <param name="path">폴더 경로</param>
    /// <param name="sprites">로드할 스프라이트 값</param>
    private void LoadSprites(string path, Dictionary<string, Sprite> sprites)
    {
        Sprite[] objs = Resources.LoadAll<Sprite>(path);
        foreach (Sprite obj in objs)
        {
            sprites[obj.name] = obj;
        }
    }
    /// <summary>
    /// string key를 기반으로 스프라이트 가져오기
    /// </summary>
    /// <param name="spriteName"></param>
    /// <returns></returns>
    public Sprite GetSprite(string spriteName)
    {
        if (!sprites.TryGetValue(spriteName, out Sprite sprite)) return null;
        return sprite;
    }

    #endregion

    #region AudioClip

    private void LoadAudioClip(string path, Dictionary<string, AudioClip> audios)
    {
        AudioClip[] objs = Resources.LoadAll<AudioClip>(path);
        foreach (AudioClip obj in objs)
        {
            audios[obj.name] = obj;
        }
    }

    public AudioClip GetAudioClip(string path)
    {
        if(!audios.TryGetValue(path, out AudioClip audio)) return null;
        return audio;
    }

    #endregion

    #region Pool
    /// <summary>
    /// 오브젝트 생성 pooling = true일 경우 풀링
    /// </summary>
    /// <param name="key"></param>
    /// <param name="parent"></param>
    /// <param name="pooling"></param>
    /// <returns></returns>
    public GameObject InstantiatePrefab(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = GetObject(key);
        if (prefab == null)
        {
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }

        if (pooling) return PoolManager.Instance.Pop(prefab);

        GameObject obj = UnityEngine.Object.Instantiate(prefab, parent);
        obj.name = prefab.name;
        return obj;
    }

    // 해당 오브젝트를 풀에 돌려놓거나 파괴한다.
    public void Destroy(GameObject obj)
    {
        if (obj == null) return;

        if (PoolManager.Instance.Push(obj)) return;

        Object.Destroy(obj);
    }

    #endregion

    #region InitOnLoad

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitApplication()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("InitOnLoad");

        if (prefabs.Length > 0)
        {
            foreach (var prefab in prefabs)
            {
                GameObject go = Object.Instantiate(prefab);
                go.name = prefab.name;
                Object.DontDestroyOnLoad(go);
            }
        }
    }

    #endregion
}
