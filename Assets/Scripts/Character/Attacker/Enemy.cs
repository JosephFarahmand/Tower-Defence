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
    EnemyController EnemyController;

    [SerializeField] private CharacterType type;

    public float damage = 1;
    [SerializeField] private float maxHealth;
    private float health;
    [SerializeField] private VirtualHealthBar virtualHealthBar;

    [SerializeField] private PathNavigation pathNavigation;

    [SerializeField] private CharacterAnimationController animationController;

    public CharacterType Type { get => type; }

    private void Awake()
    {
        pathNavigation ??= GetComponent<PathNavigation>();
        animationController ??= GetComponentInChildren<CharacterAnimationController>();
    }

    private void Reset()
    {
        pathNavigation ??= GetComponent<PathNavigation>();
        animationController ??= GetComponentInChildren<CharacterAnimationController>();
    }

    private void Start()
    {
        EnemyController = GameManager.Instance.EnemyController;
        EnemyController.Assign(this);

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

        pathNavigation.enabled = false;

        animationController.DeadAnimation();
    }

    #region Animation Event

    public void DestroyAterDead()
    {
        Destroy(gameObject);
    }

    #endregion

    public void SetPath(Transform[] points)
    {
        pathNavigation.SetPath(points);
    }

    private void OnDestroy()
    {
        EnemyController.Unassign(this);
    }
}