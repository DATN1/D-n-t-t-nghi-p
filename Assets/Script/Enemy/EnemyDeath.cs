using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private EnemyWaveSpawner spawner;

    public void SetSpawner(EnemyWaveSpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    // Gọi khi enemy chết
    public void Kill()
    {
        spawner?.OnEnemyKilled();
        Destroy(gameObject);
    }
}
