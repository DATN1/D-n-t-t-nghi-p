using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyWaveSpawner : MonoBehaviour
{
    [Header("Cấu hình spawn")]
    public Transform player;
    public float spawnRadius = 15f;
    public float timeBetweenWaves = 10f;
    public int enemiesPerWaveStart = 3;
    public float delayBetweenSpawns = 0.3f;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    [Header("Boss")]
    public int bossWaveInterval = 5; // Mỗi 5 wave có 1 Boss

    void Start()
    {
        if (player == null)
        {
            Debug.LogError(" [EnemyWaveSpawner] Chưa gán 'player'!");
            return;
        }

        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            yield return StartCoroutine(SpawnWave());

            while (enemiesAlive > 0)
            {
                yield return null;
            }

            Debug.Log($"[Wave {currentWave}] hoàn tất! Chuyển wave sau {timeBetweenWaves} giây...");
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }


    IEnumerator SpawnWave()
    {
        currentWave++;
        int enemiesThisWave = enemiesPerWaveStart + currentWave * 2;

        Debug.Log($"[Wave {currentWave}] bắt đầu! Sẽ spawn {enemiesThisWave} quái.");

        for (int i = 0; i < enemiesThisWave; i++)
        {
            SpawnOneEnemy(i + 1);
            yield return new WaitForSeconds(delayBetweenSpawns);
        }

        if (currentWave % bossWaveInterval == 0)
        {
            yield return new WaitForSeconds(1f);
<<<<<<< HEAD

            int bossCount = Mathf.FloorToInt(currentWave / bossWaveInterval);
            Debug.Log($"Wave {currentWave} - Spawning {bossCount} Boss!");

            for (int i = 0; i < bossCount; i++)
            {
                SpawnBoss(); // đảm bảo bạn có định nghĩa đúng hàm này
                yield return new WaitForSeconds(0.5f);
            }
=======
            Debug.Log("Boss đang đến!!!");
            SpawnBoss();
>>>>>>> 2c9431f406680a1057d899ea34f985fc65f63359
        }
    }

    void SpawnOneEnemy(int index)
    {
        Vector3 dir = Random.onUnitSphere;
        dir.y = 0;
        Vector3 spawnPos = player.position + dir.normalized * spawnRadius;

        if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            GameObject enemy = ObjectPool.Instance.SpawnFromPool("Enemy", hit.position, Quaternion.identity);
            if (enemy == null)
            {
                Debug.LogWarning($" Không thể spawn Enemy {index} (pool trả về null)");
                return;
            }

            enemiesAlive++;
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.SetSpawner(this);
            }

            Debug.Log($"Enemy {index} spawn tại {hit.position}");
        }
        else
        {
            Debug.LogWarning($"Không tìm được vị trí spawn cho Enemy {index}");
        }
    }

    void SpawnBoss()
    {
        Vector3 dir = Random.onUnitSphere;
        dir.y = 0;
        Vector3 spawnPos = player.position + dir.normalized * spawnRadius;

        if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            GameObject boss = ObjectPool.Instance.SpawnFromPool("Boss", hit.position, Quaternion.identity);
            if (boss == null)
            {
                Debug.LogWarning("⚠Không thể spawn Boss (pool trả về null)");
                return;
            }

            enemiesAlive++;
            EnemyHealth health = boss.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.SetSpawner(this);
            }

            Debug.Log("Boss đã được spawn!");
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }
}
