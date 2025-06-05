using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;                  // Sát thương mỗi lần tấn công
    public float attackCooldown = 1f;       // Thời gian giữa 2 lần tấn công
    private bool canAttack = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canAttack)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
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
