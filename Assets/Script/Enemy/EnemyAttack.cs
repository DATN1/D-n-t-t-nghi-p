using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    public float attackCooldown = 1f;
    private bool canAttack = true;

    private void OnTriggerStay(Collider other)
    {
        TryAttack(other.gameObject);
    }

    // ✅ Hàm có thể gọi trong test
    public void TryAttack(GameObject target)
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        if (target.CompareTag("Player") && canAttack)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                canAttack = false;
                Invoke(nameof(ResetAttack), attackCooldown);
            }
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
