using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Ammo
{
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);

            return;
        }

        var dir = targetPosition - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            if (target.TryGetComponent(out Enemy enemy))
            {
                TakeDamageToEnemy(enemy);
                ShowImpactEffect();
                Destroy(gameObject);
            }
            return;
        }

        transform.LookAt(target);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
