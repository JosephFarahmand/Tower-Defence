using Lindon.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GamePage : UIPage
{
    WaveController waveController;

    [SerializeField] private TMP_Text waveCounterText;
    [SerializeField] private TMP_Text timerText;

    [SerializeField] private Button gameSpeedButton;

    [SerializeField] private Slider healthBar;

    protected override void SetValues()
    {

    }

    protected override void SetValuesOnSceneLoad()
    {
        waveController = GameManager.Instance.EnviromentController.waveController;
        waveController.OnStartWaveCallback += waveCounter;
        waveCounterText.SetText($"0/{waveController.TotalWaveCount}");

        var stats = GameManager.Instance.Stats;
        stats.onTackDamage += updateHealthBar;
        healthBar.maxValue = stats.MaxHealth;
        healthBar.value = stats.MaxHealth;
    }

    private void updateHealthBar(float currentHealth)
    {
        healthBar.value = currentHealth;
    }

    private void waveCounter(int waveIndex)
    {
        waveCounterText.SetText($"{waveIndex}/{waveController.TotalWaveCount}");
    }
}
