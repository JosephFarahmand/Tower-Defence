using Lindon.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePage : UIPage
{
    [SerializeField] private TMP_Text waveCounterText;
    [SerializeField] private TMP_Text timerText;

    [SerializeField] private Button gameSpeedButton;

    protected override void SetValues()
    {

    }

    protected override void SetValuesOnSceneLoad()
    {
        GameManager.Instance.EnviromentController.waveController.OnStartWaveCallback += waveCounter;
    }

    private void waveCounter(int waveIndex, int totalWave)
    {
        waveCounterText.SetText($"{waveIndex + 1}/{totalWave}");
    }
}
