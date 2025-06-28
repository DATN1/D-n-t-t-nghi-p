using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
<<<<<<< HEAD
        public bool canExpand = true;
=======
>>>>>>> 2c9431f406680a1057d899ea34f985fc65f63359
    }

    public static ObjectPool Instance;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
<<<<<<< HEAD
            Debug.LogWarning("Không tìm thấy pool với tag: " + tag);
            return null;
        }

        Queue<GameObject> objectQueue = poolDictionary[tag];

        foreach (var obj in objectQueue)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        // Nếu không có object nào inactive → kiểm tra mở rộng
        Pool poolConfig = pools.Find(p => p.tag == tag);
        if (poolConfig != null && poolConfig.canExpand)
        {
            GameObject newObj = Instantiate(poolConfig.prefab, position, rotation);
            newObj.SetActive(true);
            objectQueue.Enqueue(newObj);
            return newObj;
        }

        return null;
=======
            Debug.LogError($"Pool chưa có tag: '{tag}'");
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();

        if (obj == null)
        {
            Debug.LogError($" ObjectPool[{tag}] trả về null!");
            return null;
        }

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(obj);

        return obj;
>>>>>>> 2c9431f406680a1057d899ea34f985fc65f63359
    }
    public void BuildPools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

}
