using NaughtyAttributes;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : Ammo
{
    [SerializeField,Tag] private string groundTag;

    [SerializeField] float explosionRadius = 5.0f;
    [SerializeField] float explosionPower = 2000.0f;
    Rigidbody _rigid;

    private void OnEnable()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    public override void Seek(Transform _target)
    {
        base.Seek(_target);

        ShootWithGravity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        ShowImpactEffect();

        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent(out Enemy enemy))
                TakeDamageToEnemy(enemy);
        }

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    void ShootWithGravity()
    {
        var distance = targetPosition - transform.position;

        var height = distance.y;
        var halfRange = new Vector3((distance.x + Random.Range(-0.3f, 0.3f)) / 2, 0, (distance.z + Random.Range(-0.3f, 0.3f)) / 2);

        var Vy = Mathf.Sqrt(Mathf.Abs(-2 * Physics.gravity.y * height));
        var VXZ = -(halfRange * Physics.gravity.y) / Vy;

        var velocity = new Vector3(VXZ.x, Vy, VXZ.z);

        
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

    private void FixedUpdate()
    {
        transform.LookAt(transform.position + _rigid.velocity);
    }
}
