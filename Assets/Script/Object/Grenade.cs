using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 20f;
    public float explosionRadius = 3f;
    public int explosionDamage = 50;
    public GameObject fireZonePrefab;
    public GameObject explosionEffect;

    private Rigidbody rb;
    private Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindNearestEnemy();

        if (target == null)
        {
            Debug.LogWarning("❌ Không tìm thấy enemy để ném!");
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position + Vector3.up * 0.5f - transform.position).normalized; // Hướng tới giữa thân enemy
        rb.velocity = dir * speed;

        Collider playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        Physics.IgnoreCollision(GetComponent<Collider>(), playerCol);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] hitTargets = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hitTargets)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
            {
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                if (eh != null)
                {
                    eh.TakeDamage(explosionDamage);
                    Debug.Log($"{hit.name} trúng fireball, mất {explosionDamage} máu!");
                }
            }
        }

        if (fireZonePrefab != null)
        {
            Instantiate(fireZonePrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var obj in enemies)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = obj.transform;
            }
        }

        foreach (var obj in bosses)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = obj.transform;
            }
        }

        return nearest;
    }
}
