using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string _monsterName;
        public GameObject _monsterPrefab;
    }

    public static MonsterPool Instance;

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, Transform> containerDictionary = new Dictionary<string, Transform>();

    private void Awake()
    {
        Instance = this;
        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        containerDictionary = new Dictionary<string, Transform>();

        GameObject[] _monsterPrefabs = Resources.LoadAll<GameObject>("Enemies/Monsters");
        
        foreach (GameObject prefab in _monsterPrefabs)
        {
            int poolSize = 10;
            Queue<GameObject> objectPool = new Queue<GameObject>();

            GameObject container = new GameObject(prefab.name + "Container");
            container.transform.SetParent(this.transform);

            containerDictionary[prefab.name] = container.transform;

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab, container.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(prefab.name, objectPool);
        }
    }

        public GameObject SpawnFromPool(string _monsterName, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(_monsterName) || poolDictionary[_monsterName].Count == 0)
            {
                Debug.LogWarning("ERROR");
                return null;
            }

            if (!containerDictionary.TryGetValue(_monsterName, out Transform containerTransform))
            {
                Debug.LogError("ERROR: Container not found for " + _monsterName);
                return null;
            }
        
            GameObject objectToSpawn = poolDictionary[_monsterName].Dequeue();

            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.transform.SetParent(containerTransform);
            objectToSpawn.SetActive(true);

            poolDictionary[_monsterName].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

    public void ReturnToPool(string _monsterName, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(_monsterName))
        {
            Debug.Log("Error");
            return;
        }

        objectToReturn.SetActive(false);
        poolDictionary[_monsterName].Enqueue(objectToReturn);
    }
}
