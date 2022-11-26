using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    [Header("Info")]
    public TowerType Type;
    public TowerLevel Level;

    [Header("Target")]
    [SerializeField, Tag] private string targetTag;
    [SerializeField] private float maxRange = 15f;
    [SerializeField] private float minRange = 15f;
    protected List<Transform> targets;
    private Transform target;

    [Header("Fire")]
    [SerializeField] private float fireRate = 1;
    private float fireCountdown = 0;

    private void Start()
    {
        targets = new List<Transform>();

        var sphereCollider = gameObject.GetOrAddComponent<SphereCollider>();
        sphereCollider.radius = maxRange;
        sphereCollider.center = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRange);
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }

    private void OnValidate()
    {
        var sphereCollider = gameObject.GetOrAddComponent<SphereCollider>();
        sphereCollider.radius = maxRange;
        sphereCollider.center = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        // add to list
        if (other.CompareTag(targetTag))
        {
            targets.Add(other.transform);

            target = transform.GetClosestTarget(targets, minRange, maxRange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // remove from list
        if (other.CompareTag(targetTag) && targets.Contains(other.transform))
        {
            targets.Remove(other.transform);
        }
    }

    protected virtual void Update()
    {
        if (targets.Count == 0) return;

        SetTarget();

        ReadyToShoot();
    }

    private void SetTarget()
    {
        if (targets.Count > 0)
        {
            target = transform.GetClosestTarget(targets, minRange, maxRange);
        }
    }

    private void ReadyToShoot()
    {
        if (fireCountdown <= 0)
        {
            Shoot(target);
            fireCountdown = 1 / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    protected abstract void Shoot(Transform target);

    public Transform GetTarget() => targets.RandomItem();
}
