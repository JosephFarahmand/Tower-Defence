using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    protected Vector3 targetPosition;
    protected Transform target;

    [SerializeField] protected float speed;

    [SerializeField] private float damage = 50;
    [SerializeField] private GameObject impactEffect;

    public virtual void Seek(Transform _target)
    {
        target = _target;
        if (target == null) return;
        targetPosition = target.position;
    }

    protected void TakeDamageToEnemy(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }

    protected void ShowImpactEffect()
    {
        var effectIns = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(effectIns, 2f);
    }
}
