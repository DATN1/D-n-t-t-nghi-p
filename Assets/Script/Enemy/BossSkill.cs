using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public string bossBulletTag = "BossBullet";
    public Transform firePoint;
    public float fireInterval = 3f;
    public float fireRange = 20f;
    public int bulletCount = 5;               // số đạn mỗi lần bắn
    public float spreadAngle = 45f;           // tổng góc spread

    private float timer;
    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver || player == null) return;

        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            timer = 0f;
            ShootSpread();
        }
    }

    void ShootSpread()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > fireRange) return;

        Vector3 baseDir = (player.position - firePoint.position).normalized;
        Quaternion baseRot = Quaternion.LookRotation(baseDir);

        float angleStep = spreadAngle / (bulletCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angleOffset = startAngle + i * angleStep;
            Quaternion rotation = baseRot * Quaternion.Euler(0f, angleOffset, 0f);

            GameObject bullet = ObjectPool.Instance.SpawnFromPool(bossBulletTag, firePoint.position, rotation);

            Debug.Log($" Boss bắn đạn {i + 1} theo góc {angleOffset}°");
        }
    }
}
