using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    //[SerializeField] private Crystal crystalPrefab;
    [SerializeField] private string crystalPrefabId;
    [SerializeField] private Transform firePoint;

    protected override void Shoot(Transform target)
    {
        if (target == null) return;

        //var newProjectile = Instantiate(crystalPrefab, firePoint.position, firePoint.localRotation);
        var newProjectile = ObjectPool.Instance.Get<Ammo>(crystalPrefabId);
        newProjectile.transform.SetPositionAndRotation(firePoint.position, firePoint.localRotation);
        newProjectile.Seek(target);
    }
}
