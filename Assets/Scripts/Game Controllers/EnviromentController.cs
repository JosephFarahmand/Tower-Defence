using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public WaveController waveController { get; private set; }

    [SerializeField] private List<Wave> waves;

    private void Awake()
    {
        waveController = new WaveController(this);
    }

    public List<Wave> GetWaves() => waves;

    private void Update()
    {
        waveController.Update();
    }
}

[Serializable]
public class Wave
{
    [SerializeField] private Waypoint waypoint;
    [SerializeField] private List<EnemyInfo> enemies;
    [Tooltip("Delay to spawn each enemy"),Min(0)] private float timeBetweenSpawn = 0.5f;
    [Tooltip("Delay to start this wave"),Min(0)] public float timeToStart = 5;


    public void SpawnWave()
    {
        waypoint.Spawner.SpawnWave(waypoint.Points, timeBetweenSpawn, enemies);
    }

    [System.Serializable]
    public struct EnemyInfo
    {
        public CharacterType type;
        public int count;
    }
}
