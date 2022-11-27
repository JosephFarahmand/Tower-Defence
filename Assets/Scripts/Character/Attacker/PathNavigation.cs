using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PathNavigation : MonoBehaviour
{
    [SerializeField] private float speed;

    Transform[] points;

    Transform targetPoint;
    int pointIndex = 0;

    public void SetPath(Transform[] points)
    {
        this.points = points;
        targetPoint = points[0];
    }

    private void Update()
    {
        if (points == null) return;

        var dir = targetPoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, targetPoint.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (pointIndex >= points.Length -1)
        {
            Destroy(gameObject);
            return;
        }

        targetPoint = points[++pointIndex];
        transform.LookAt(targetPoint);
    }
}
