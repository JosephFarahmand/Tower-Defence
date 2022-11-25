using UnityEngine;

public class Projectile : Ammo
{
    //private Transform target;

    //[SerializeField] private float speed;

    //[SerializeField] private GameObject impactEffect;

    //public void Seek(Transform _target)
    //{
    //    target = _target;
    //}

    //private void Update()
    //{
    //    if (target == null)
    //    {
    //    Destroy(gameObject);

    //        return;
    //    }

    //    var dir = target.position - transform.position;
    //    var distanceThisFrame = speed * Time.deltaTime;

    //    if (dir.magnitude <= distanceThisFrame)
    //    {
    //        HitTarget();
    //        return;
    //    }

    //    transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    //}

    protected override void HitTarget()
    {
        //var effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);

        base.HitTarget();
    }
}
