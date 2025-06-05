using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private EnemySpawner spawner;

    public void SetSpawner(EnemySpawner spawnerRef)
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
