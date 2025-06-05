using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootInterval = 1f;
    public float attackRange = 10f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            timer = 0f;
            ShootNearestEnemy();
        }
    }

    void ShootNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist && dist <= attackRange)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        if (nearest != null)
        {
            Vector3 dir = (nearest.transform.position - shootPoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(dir));
        }
    }
}
