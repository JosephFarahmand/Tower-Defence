using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField,Required] private WaveSpawner spawner;

    Transform[] points;
    public Transform[] Points
    {
        get
        {
            if (points == null || points.Length != transform.childCount)
            {
                points = new Transform[transform.childCount];
                for (int i = 0; i < transform.childCount; i++)
                {
                    points[i] = transform.GetChild(i).transform;
                }
            }
            return points;
        }
    }

    public WaveSpawner Spawner { get => spawner;  }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Transform child = transform.GetChild(i);
            Transform nextChild = transform.GetChild(i+1);
            Gizmos.DrawLine(child.position, nextChild.position);
        }
    }

    private void Reset()
    {
        spawner ??= GetComponentInChildren<WaveSpawner>();
    }
}
