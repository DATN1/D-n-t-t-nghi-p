using UnityEngine;
using System.Collections;

public class FireZone : MonoBehaviour
{
    public float duration = 5f;
    public int damagePerSecond = 10;

    void Start()
    {
        StartCoroutine(BurnCoroutine());
    }

    IEnumerator BurnCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 2f);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
                {
                    EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                    if (eh != null)
                    {
                        eh.TakeDamage(damagePerSecond);
                        Debug.Log($"{hit.name} bị thiêu cháy, mất {damagePerSecond} máu!");
                    }
                }
            }

            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("🔥 Vùng lửa kết thúc, FireZone bị huỷ");

        Destroy(gameObject);
    }
}
