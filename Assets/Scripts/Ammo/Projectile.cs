using NaughtyAttributes;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : Ammo
{
    [SerializeField] private GameObject impactEffect;
    [SerializeField,Tag] private string groundTag;

    [SerializeField] float explosionRadius = 5.0f;
    [SerializeField] float explosionPower = 2000.0f;

    [SerializeField] float damage = 50;

    public override void Seek(Transform _target)
    {
        base.Seek(_target);

        ShootWithGravity(_target.position);
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            HitTarget();
        }
    }

    protected override void HitTarget()
    {
        var effectIns = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(effectIns, 2f);

        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (var hit in colliders)
        {
            //if (hit.TryGetComponent(out Rigidbody rigidbody))
            //{
            //    AddExplosionForce(explosionPosition, rigidbody);
            //}
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    public void AddExplosionForce(Vector3 explosionPosition, Rigidbody targetRigidbody)
    {
        targetRigidbody.AddExplosionForce(explosionPower, explosionPosition, explosionRadius);
    }

    void ShootWithGravity(Vector3 targetPosition)
    {
        var distance = targetPosition - transform.position;

        var height = distance.y;
        var halfRange = new Vector3((distance.x + Random.Range(-0.3f, 0.3f)) / 2, 0, (distance.z + Random.Range(-0.3f, 0.3f)) / 2);

        var Vy = Mathf.Sqrt(Mathf.Abs(-2 * Physics.gravity.y * height));
        var VXZ = -(halfRange * Physics.gravity.y) / Vy;

        var velocity = new Vector3(VXZ.x, Vy, VXZ.z);

        var _rigid = GetComponent<Rigidbody>();
        _rigid.velocity = velocity;
        _rigid.useGravity = true;

    }

    public IEnumerator RotateToDirection(Transform transform, Vector3 positionToLook, float timeToRotate)
    {
        var startRotation = transform.rotation;
        var direction = positionToLook - transform.position;
        var finalRotation = Quaternion.LookRotation(direction);
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / timeToRotate;
            transform.rotation = Quaternion.Lerp(startRotation, finalRotation, t);
            yield return null;
        }
        transform.rotation = finalRotation;
    }
}
