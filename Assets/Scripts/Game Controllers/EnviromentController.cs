using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    private List<BuildingPoint>  buildingPoints = new List<BuildingPoint>();
    public WaveController waveController { get; private set; }

    [SerializeField] private List<Wave> waves;

    private void Awake()
    {
        waveController = new WaveController(this);

        buildingPoints = new List<BuildingPoint>(FindObjectsOfType<BuildingPoint>());
    }

    public void Initialization()
    {
        GameManager.Instance.Stats.OnChangeState += Stats_OnChangeState;
    }

    public List<Wave> GetWaves() => waves;

    public int TotalEnemyCount => waves.Select(x => x.EnemiesCount).Sum();

    private void Update()
    {
        waveController.Update();
    }

    private void Stats_OnChangeState(GameStats.State currentState)
    {
        if (currentState == GameStats.State.Reset)
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        waveController.ResetGame();

        buildingPoints.ForEach(x => x.ClearTower());
    }
}

[Serializable]
public class Wave
{
    [SerializeField] private Waypoint waypoint;
    [SerializeField] private List<EnemyInfo> enemies;
    [Tooltip("Delay to spawn each enemy"),Min(0)] private float timeBetweenSpawn = 0.5f;
    [Tooltip("Delay to start this wave"),Min(0)] public float timeToStart = 5;

    public int EnemiesCount => enemies.Select(x => x.count).Sum();
    public float SpawnTime => EnemiesCount * timeBetweenSpawn;

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
