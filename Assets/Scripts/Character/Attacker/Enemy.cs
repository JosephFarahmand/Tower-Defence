using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Archer,
    Mage,
    Spearman,
    Swordman
}

[RequireComponent(typeof(PathNavigation))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterType type;

    [SerializeField] private float maxHealth;
    private float health;
    [SerializeField] private VirtualHealthBar virtualHealthBar;

    [SerializeField] private PathNavigation pathNavigation;

    public CharacterType Type { get => type; }

    private void Awake()
    {
        pathNavigation ??= GetComponent<PathNavigation>();
    }

    private void Reset()
    {
        pathNavigation ??= GetComponent<PathNavigation>();
    }

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
        Reward.KillEnemy(this);
        Destroy(gameObject);
    }

    public void SetPath(Transform[] points)
    {
        pathNavigation.SetPath(points);
    }
}
