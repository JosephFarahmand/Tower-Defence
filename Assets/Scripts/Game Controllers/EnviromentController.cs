using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public List<Waypoint> Waypoints { get; private set; }

    public Transform[] GetPoints()
    {
        return Waypoints.RandomItem().Points;
    }

    private void Awake()
    {
        Waypoints = new List<Waypoint>(FindObjectsOfType<Waypoint>());
    }
}
