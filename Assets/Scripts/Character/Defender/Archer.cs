using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Archer : MonoBehaviour
{
    //[SerializeField] private Arrow arrowPrefab;
    [SerializeField] private string arrowId;
    [SerializeField] private Transform firePoint;

    Transform target;

    public void Shoot()
    {
        if (target == null) return;
        //var newProjectile = Instantiate(arrowPrefab, firePoint.position, firePoint.localRotation);

        var newProjectile = ObjectPool.Instance.Get<Ammo>(arrowId);
        newProjectile.transform.SetPositionAndRotation(firePoint.position, firePoint.localRotation);

        newProjectile.Seek(target);
    }

    public void SetLookAt(Transform _target)
    {
        if(target != null) return;
        target = _target;
    }

    private void Update()
    {
        if (target == null) return;
        transform.LookAt(target);
    }
}
