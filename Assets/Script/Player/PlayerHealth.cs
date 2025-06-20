﻿using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Cấu hình máu")]
    public int maxHealth = 100;
    private int currentHealth;
    public bool isDead { get; private set; } = false;
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Máu hiện tại: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Bị tấn công! Máu còn lại: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GameManager.Instance.EndGame();
        Debug.Log("Người chơi đã chết.");
        
    }
    public void SetHealthForTest(int hp)
    {
        currentHealth = hp;
        isDead = false;
    }

    public void SetDead()
    {
        isDead = true;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
