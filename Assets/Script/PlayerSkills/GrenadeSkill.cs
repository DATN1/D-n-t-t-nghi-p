using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class GrenadeSkill : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public Button grenadeButton;
    public float cooldown = 20f;
    public Image cooldownImage;

    private bool canThrow = true;
    private float cooldownTimer = 0f;

    void Start()
    {
        grenadeButton.onClick.AddListener(ThrowGrenade);
        cooldownImage.fillAmount = 0f;
    }

    void Update()
    {
        if (!canThrow)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownImage.fillAmount = cooldownTimer / cooldown;

            if (cooldownTimer <= 0f)
            {
                canThrow = true;
                cooldownImage.fillAmount = 0f;
                grenadeButton.interactable = true;
            }
        }
    }

    void ThrowGrenade()
    {
        if (!canThrow) return;

        Transform target = FindNearestEnemy();
        if (target == null) return;

        Vector3 dir = (target.position - throwPoint.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);

        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, rot);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.velocity = dir * 15f; // tốc độ bay giống đạn

        Physics.IgnoreCollision(grenade.GetComponent<Collider>(), GetComponent<Collider>());

        canThrow = false;
        cooldownTimer = cooldown;
        grenadeButton.interactable = false;
    }

    Transform FindNearestEnemy()
    {
        List<GameObject> allTargets = new List<GameObject>();
        allTargets.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        allTargets.AddRange(GameObject.FindGameObjectsWithTag("Boss"));

        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var t in allTargets)
        {
            float dist = Vector3.Distance(transform.position, t.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = t.transform;
            }
        }

        return nearest;
    }
}
