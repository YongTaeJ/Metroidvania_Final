using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterPoolManager : Singleton<MonsterPoolManager>
{
    [System.Serializable]
    public class Pool
    {
        private string _monsterName;
        private GameObject _monsterPrefab;
        private IObjectPool<GameObject> _pool;
        private Transform _root;

        private Transform Root
        {
            get
            {
                if (_root == null)
                {
                    GameObject obj = new() { name = $"MonsterPool" };
                    _root = obj.transform;
                }
                return _root;
            }
        }
        public Pool(GameObject _monsterPrefab)
        {
            this._monsterPrefab = _monsterPrefab;
            this._pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
        }

        public GameObject Pop()
        {
            return _pool.Get();
        }

        public void push(GameObject obj)
        {
            _pool.Release(obj);
        }

        private GameObject OnCreate()
        {
            GameObject obj = GameObject.Instantiate(_monsterPrefab);
            obj.transform.SetParent(Root);
            obj.name = _monsterName;
            return obj;
        }

        private void OnGet(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnRelease(GameObject obj)
        {
            obj.SetActive(false);
        }
        private void OnDestroy(GameObject obj)
        {
            GameObject.Destroy(obj);
        }
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, Transform> containerDictionary = new Dictionary<string, Transform>();

    public override bool Initialize()
    {
        if (base.Initialize())
        {
            InitializePools();
        }
        return true;
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
            return null;
        }

        if (!containerDictionary.TryGetValue(_monsterName, out Transform containerTransform))
        {
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[_monsterName].Dequeue();
        
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.SetParent(containerTransform);
        objectToSpawn.SetActive(true);
        objectToSpawn.GetComponent<EnemyStateMachine>().AppearMonster();

        poolDictionary[_monsterName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnToPool(string _monsterName, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(_monsterName))
        {
            return;
        }

        var enemyStateMachine = objectToReturn.GetComponent<EnemyStateMachine>();
        if (enemyStateMachine != null)
        {
            enemyStateMachine.DisappearMonster();
        }

        objectToReturn.SetActive(false);
        poolDictionary[_monsterName].Enqueue(objectToReturn);
    }
}
