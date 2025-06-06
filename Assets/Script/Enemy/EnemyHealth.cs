using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private EnemyWaveSpawner spawner;

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (GameManager.Instance.isGameOver) return;

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} bị bắn! Máu còn: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetSpawner(EnemyWaveSpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} chết");
        spawner?.OnEnemyKilled();
        gameObject.SetActive(false);
    }
}
