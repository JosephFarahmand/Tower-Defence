using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    protected override void Shoot(Transform target)
    {
        if (target == null) return;
        var newProjectile =  Instantiate(projectilePrefab, firePoint.position, firePoint.localRotation);
        newProjectile.Seek(target);
    }
}