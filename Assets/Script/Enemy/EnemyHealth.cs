using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private EnemyWaveSpawner spawner;

    [Header("Drop Settings")]
    public GameObject[] dropItems; 
    [Range(0f, 1f)]
    public float dropChance = 0.5f; 
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
        TryDropItem();
        spawner?.OnEnemyKilled();
        gameObject.SetActive(false);
    }
    void TryDropItem()
    {
        if (dropItems == null || dropItems.Length == 0)
        {
            Debug.Log($"{gameObject.name}: Không có vật phẩm nào để rớt.");
            return;
        }

        float roll = Random.value;
        Debug.Log($"{gameObject.name}: Thử rớt vật phẩm... Kết quả roll = {roll} (cần <= {dropChance})");

        if (roll <= dropChance)
        {
            int randomIndex = Random.Range(0, dropItems.Length);
            GameObject drop = dropItems[randomIndex];
            Instantiate(drop, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name}: Rớt vật phẩm {drop.name} tại vị trí {transform.position}");
        }
        else
        {
            Debug.Log($"{gameObject.name}: Không rớt vật phẩm.");
        }
    }
}
