using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolTests
{
    private ObjectPool pool;
    private GameObject prefab;
    private string testTag = "TestBullet";

    [SetUp]
    public void SetUp()
    {
        // Tạo object pool
        GameObject poolGO = new GameObject("ObjectPool");
        pool = poolGO.AddComponent<ObjectPool>();
        ObjectPool.Instance = pool;

        // Tạo prefab giả
        prefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        prefab.name = "TestBullet";
        prefab.SetActive(false);

        pool.pools = new List<ObjectPool.Pool>
        {
            new ObjectPool.Pool
            {
                tag = testTag,
                prefab = prefab,
                size = 3
            }
        };

        pool.BuildPools();
    }

    [Test]
    public void BuildPools_CreatesCorrectAmount()
    {
        Assert.IsTrue(pool.poolDictionary.ContainsKey(testTag), "Không có tag trong dictionary.");
        Assert.AreEqual(3, pool.poolDictionary[testTag].Count, "Số lượng object trong pool sai.");
    }


    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(pool.gameObject);
        Object.DestroyImmediate(prefab);
    }
}
