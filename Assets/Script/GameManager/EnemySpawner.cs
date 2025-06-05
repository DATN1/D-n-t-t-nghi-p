using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Cấu hình Spawn")]
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRadius = 15f;
    public float spawnInterval = 3f;
    public int maxEnemyCount = 10;

    private int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (player == null || enemyPrefab == null) return;

        if (currentEnemyCount >= maxEnemyCount) return;

        Vector3 spawnDirection = Random.onUnitSphere;
        spawnDirection.y = 0f; // mặt đất
        Vector3 spawnPos = player.position + spawnDirection.normalized * spawnRadius;

        // Bảo đảm quái spawn trên vùng NavMesh
        if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 3f, NavMesh.AllAreas))
        {
            GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            currentEnemyCount++;

            // Giảm số lượng khi enemy chết (tùy bạn)
            enemy.GetComponent<EnemyDeath>()?.SetSpawner(this);
        }
    }

    public void OnEnemyKilled()
    {
        currentEnemyCount--;
        currentEnemyCount = Mathf.Clamp(currentEnemyCount, 0, maxEnemyCount);
    }
}
