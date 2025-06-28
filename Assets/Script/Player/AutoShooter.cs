using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public Transform shootPoint;
    public float shootInterval = 1f;
    public float attackRange = 10f;
    private float timer;

    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            timer = 0f;
            ShootNearestTarget();
        }
    }

    void ShootNearestTarget()
    {
        List<GameObject> allTargets = new List<GameObject>();
        allTargets.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        allTargets.AddRange(GameObject.FindGameObjectsWithTag("Boss"));

        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var target in allTargets)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist < minDist && dist <= attackRange)
            {
                minDist = dist;
                nearest = target;
            }
        }

        if (nearest != null)
        {
            Vector3 dir = (nearest.transform.position - shootPoint.position).normalized;
            ObjectPool.Instance.SpawnFromPool("Bullet", shootPoint.position, Quaternion.LookRotation(dir));
        }
    }
}
