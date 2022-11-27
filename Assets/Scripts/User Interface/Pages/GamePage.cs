using Lindon.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePage : UIPage
{
    WaveController waveController;

    [SerializeField] private TMP_Text waveCounterText;
    [SerializeField] private TMP_Text timerText;

    [SerializeField] private Button gameSpeedButton;

    protected override void SetValues()
    {

    }

    protected override void SetValuesOnSceneLoad()
    {
        waveController = GameManager.Instance.EnviromentController.waveController;
        waveController.OnStartWaveCallback += waveCounter;
        waveCounterText.SetText($"0/{waveController.TotalWaveCount}");
    }

    private void waveCounter(int waveIndex)
    {
        waveCounterText.SetText($"{waveIndex}/{waveController.TotalWaveCount}");
    }
}
