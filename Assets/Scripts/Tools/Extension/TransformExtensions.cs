using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TransformExtensions
{
    public static void DestroyChildren(this Transform transform)
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }

    public static void UnparentChildren(this Transform transform)
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).SetParent(null);
        }
    }

    public static Transform Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }

    public static Transform GetClosestTarget(this Transform transform, List<Transform> targets)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in targets)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    public static Transform GetClosestTarget(this Transform transform, Transform[] targets)
    {
        return GetClosestTarget(transform, targets.ToList());
    }

    public static Transform GetClosestTarget(this Transform transform, List<Transform> targets, float min, float max)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // calculate squared distances
        min *= min;
        max *= max;
        foreach (Transform potentialTarget in targets)
        {
            if (potentialTarget == null) continue;
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && dSqrToTarget >= min && dSqrToTarget <= max)
            {
                bestTarget = potentialTarget;
                closestDistanceSqr = dSqrToTarget;
            }
        }
        return bestTarget;
    }

    public static Transform GetClosestTarget(this Transform transform, Transform[] targets, float min, float max)
    {
        return GetClosestTarget(transform, targets.ToList(), min, max);
    }
}
