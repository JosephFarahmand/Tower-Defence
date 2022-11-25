using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Archer : MonoBehaviour
{
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform firePoint;

    public void Shoot(Transform target)
    {
        var newProjectile = Instantiate(arrowPrefab, firePoint.position, firePoint.localRotation);
        newProjectile.Seek(target);
    }
}
