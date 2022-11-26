using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    [SerializeField] private VirtualHealthBar virtualHealthBar;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        virtualHealthBar.SetValue(health / maxHealth);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        virtualHealthBar.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
