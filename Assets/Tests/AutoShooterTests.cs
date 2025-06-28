using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;

public class AutoShooterTests
{
    private GameObject shooterGO;
    private AutoShooter shooter;
    private GameObject enemyGO;
    private ObjectPool pool;
    private GameObject bulletPrefab;
    private GameObject shootPointGO;

    [SetUp]
    public void SetUp()
    {
        // Tạo shooter
        shooterGO = new GameObject("Shooter");
        shooter = shooterGO.AddComponent<AutoShooter>();

        // Tạo shootPoint
        shootPointGO = new GameObject("ShootPoint");
        shootPointGO.transform.SetParent(shooterGO.transform);
        shootPointGO.transform.localPosition = Vector3.zero;
        shooter.shootPoint = shootPointGO.transform;
        shooter.attackRange = 10f;
        shooter.shootInterval = 0.1f;

        // Tạo enemy
        enemyGO = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        enemyGO.name = "Enemy";
        enemyGO.tag = "Enemy";
        enemyGO.transform.position = shooterGO.transform.position + Vector3.forward * 5;

        // Tạo ObjectPool
        GameObject poolGO = new GameObject("ObjectPool");
        pool = poolGO.AddComponent<ObjectPool>();
        ObjectPool.Instance = pool;

        // Tạo prefab đạn
        bulletPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bulletPrefab.name = "Bullet";
        bulletPrefab.SetActive(false);

        pool.pools = new List<ObjectPool.Pool>
        {
            new ObjectPool.Pool
            {
                tag = "Bullet",
                prefab = bulletPrefab,
                size = 5
            }
        };

        pool.BuildPools();

        // Tạo GameManager giả nếu chưa có
        if (GameManager.Instance == null)
        {
            var gmGO = new GameObject("GameManager");
            gmGO.AddComponent<FakeGameManager>();
        }
    }

    [UnityTest]
    public IEnumerator AutoShooter_SpawnsBullet_WhenEnemyInRange()
    {
        int before = CountBullets();

        yield return new WaitForSeconds(0.2f);

        int after = CountBullets();

        Assert.Greater(after, before, "AutoShooter phải bắn ra ít nhất 1 viên đạn.");
    }

    [UnityTest]
    public IEnumerator AutoShooter_DoesNotShoot_WhenNoTargets()
    {
        GameObject.DestroyImmediate(enemyGO);

        int before = CountBullets();
        yield return new WaitForSeconds(0.2f);
        int after = CountBullets();

        Assert.AreEqual(before, after, "Không có enemy → không được bắn.");
    }

    private int CountBullets()
    {
        int count = 0;
        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.name.Contains("Bullet") && go.activeInHierarchy)
                count++;
        }
        return count;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(shooterGO);
        Object.DestroyImmediate(enemyGO);
        Object.DestroyImmediate(pool.gameObject);
        Object.DestroyImmediate(bulletPrefab);
        Object.DestroyImmediate(shootPointGO);
    }
}
