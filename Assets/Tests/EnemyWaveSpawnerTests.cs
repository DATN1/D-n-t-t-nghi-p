using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;

public class EnemyWaveSpawnerTests
{
    private GameObject spawnerGO;
    private EnemyWaveSpawner spawner;
    private GameObject playerGO;
    private ObjectPool pool;
    private GameObject enemyPrefab;
    private GameObject bossPrefab;

    [SetUp]
    public void SetUp()
    {
        // Tạo Player
        playerGO = new GameObject("Player");
        playerGO.tag = "Player";
        playerGO.transform.position = Vector3.zero;

        // Tạo EnemyWaveSpawner
        spawnerGO = new GameObject("Spawner");
        spawner = spawnerGO.AddComponent<EnemyWaveSpawner>();
        spawner.player = playerGO.transform;
        spawner.enemiesPerWaveStart = 1; // dễ test
        spawner.delayBetweenSpawns = 0.01f;
        spawner.bossWaveInterval = 2;

        // Tạo prefab enemy giả
        enemyPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        enemyPrefab.name = "Enemy";
        enemyPrefab.tag = "Enemy";
        enemyPrefab.AddComponent<EnemyHealth>();

        // Tạo prefab boss giả
        bossPrefab = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        bossPrefab.name = "Boss";
        bossPrefab.tag = "Boss";
        bossPrefab.AddComponent<EnemyHealth>();

        // ObjectPool setup
        var poolGO = new GameObject("ObjectPool");
        pool = poolGO.AddComponent<ObjectPool>();
        ObjectPool.Instance = pool;

        pool.pools = new List<ObjectPool.Pool>
        {
            new ObjectPool.Pool { tag = "Enemy", prefab = enemyPrefab, size = 10 },
            new ObjectPool.Pool { tag = "Boss", prefab = bossPrefab, size = 5 }
        };

        pool.BuildPools();
    }

    [UnityTest]
    public IEnumerator SpawnWave_CreatesEnemies()
    {
        yield return spawner.StartCoroutine(spawnerTestWrapper_SpawnWave());

        int count = CountObjectsByName("Enemy");
        Assert.Greater(count, 0, "Enemy phải được spawn trong wave.");
    }

    [UnityTest]
    public IEnumerator SpawnBoss_CreatesBoss()
    {
        // Tạo wave giả để boss xuất hiện
        for (int i = 1; i < spawner.bossWaveInterval; i++)
            spawner.OnEnemyKilled(); // giả lập wave trước hoàn tất

        yield return spawner.StartCoroutine(spawnerTestWrapper_SpawnWave());

        int count = CountObjectsByName("Boss");
        Assert.Greater(count, 0, "Boss phải được spawn đúng wave.");
    }

    [Test]
    public void OnEnemyKilled_DecreasesEnemiesAlive()
    {
        spawner.OnEnemyKilled();
        spawner.OnEnemyKilled(); // 2 lần gọi
        Assert.AreEqual(0, spawnerGO.GetComponent<EnemyWaveSpawner>().GetType()
            .GetField("enemiesAlive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(spawnerGO.GetComponent<EnemyWaveSpawner>()));
    }

    private IEnumerator spawnerTestWrapper_SpawnWave()
    {
        // chạy SpawnWave() một cách an toàn
        var method = typeof(EnemyWaveSpawner).GetMethod("SpawnWave", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        IEnumerator coroutine = (IEnumerator)method.Invoke(spawner, null);
        while (coroutine.MoveNext()) yield return coroutine.Current;
    }

    private int CountObjectsByName(string name)
    {
        int count = 0;
        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.name.Contains(name) && go.activeInHierarchy)
                count++;
        }
        return count;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(spawnerGO);
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(pool.gameObject);
        Object.DestroyImmediate(enemyPrefab);
        Object.DestroyImmediate(bossPrefab);
    }
}
