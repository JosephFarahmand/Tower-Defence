using System.Collections.Generic;
using UnityEngine;

public class WaveController
{
    public delegate void OnStartWave(int waveIndex);
    public OnStartWave OnStartWaveCallback;

    private List<Wave> waves;
    Wave currentWave;

    private float countdown = 2;

    public int TotalWaveCount { get; private set; }
    private int WaveNumber = 0;

    public WaveController(EnviromentController enviroment)
    {
        waves = enviroment.GetWaves();
        TotalWaveCount = waves.Count;

        currentWave = waves[WaveNumber];
        countdown = currentWave.timeToStart;
    }

    public void Update()
    {
        if (WaveNumber >= TotalWaveCount) return;
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                currentWave.SpawnWave();

                WaveNumber++;

                // invode callback
                OnStartWaveCallback?.Invoke(WaveNumber);

                if (WaveNumber < TotalWaveCount)
                {
                    // next wave
                    currentWave = waves[WaveNumber];
                    countdown = waves[WaveNumber].timeToStart + waves[WaveNumber - 1].SpawnTime;
                }
            }
        }
    }

    public void ResetGame()
    {
        WaveNumber = 0;

        currentWave = waves[WaveNumber];
        countdown = currentWave.timeToStart;

        OnStartWaveCallback?.Invoke(WaveNumber);
    }
}