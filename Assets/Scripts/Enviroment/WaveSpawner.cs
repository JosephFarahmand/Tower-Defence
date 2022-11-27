using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Wave;

public class WaveSpawner : MonoBehaviour
{
    GameData gameData;

    private float timeBetweenSpawn;

    Dictionary<CharacterType,int> enemiesInfo;

    int totalEnemy = 0;

    private void Start()
    {
        gameData = GameManager.Instance.GameData;
    }

    public void SpawnWave(Transform[] points, float timeBetweenSpawn, List<EnemyInfo> enemiesInfo)
    {
        this.enemiesInfo = new Dictionary< CharacterType, int>();
        foreach (var info in enemiesInfo)
        {
            this.enemiesInfo.Add(info.type, info.count);
            totalEnemy += info.count;
        }

        this.timeBetweenSpawn = timeBetweenSpawn;
        StartCoroutine(SpawnWaveCorotine(points));
    }

    private IEnumerator SpawnWaveCorotine(Transform[] points)
    {
        Debug.Log("Wave Incomming!");

        for (int i = 0; i < totalEnemy; i++)
        {
            SpawnEnemy(points);

            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private void SpawnEnemy(Transform[] points)
    {
        var prefab = GetEnemy();
        if (prefab == null) return;
        var enemyIns = Instantiate(prefab, transform.position, Quaternion.identity);
        enemyIns.SetPath(points);
    }

    public Enemy GetEnemy()
    {
        if (enemiesInfo.Count == 0) return null;
        var info = enemiesInfo.RandomItem();
        if (info.Value > 0)
        {
            enemiesInfo[info.Key]--;
            return gameData.GetEnemy(info.Key);
        }
        else
        {
            enemiesInfo.Remove(info.Key);
            return GetEnemy();
        }
    }
}
