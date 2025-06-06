using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public float lifetime = 5f;

    private float lifeTimer;

    void OnEnable()
    {
        lifeTimer = lifetime;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth hp = other.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
                Debug.Log("💥 Player trúng đạn Boss");
            }

            gameObject.SetActive(false);
        }
    }
}
