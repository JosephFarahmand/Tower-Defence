using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Ammo
{
    private void Update()
    {
        if (target == null)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
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
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            return;
        }

        transform.LookAt(target);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
