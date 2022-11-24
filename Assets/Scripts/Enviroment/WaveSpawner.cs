using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float timeBetweenWaves = 5;
    private float countdown = 2;

    private int waveNumber = 1;

    private void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }
        }
    }

    private IEnumerator SpawnWave()
    {
        Debug.Log("Wave Incomming!");

        for(int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(0.5f);
        }

        waveNumber++;
    }

    private void SpawnEnemy()
    {
        var prefab = enemyPrefabs.RandomItem();

        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
