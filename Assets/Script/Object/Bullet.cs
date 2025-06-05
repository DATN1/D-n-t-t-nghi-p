using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime); // tự hủy sau X giây
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth eh = other.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(damage);
            }
        }

        Destroy(gameObject); // Hủy viên đạn sau va chạm
    }
}
