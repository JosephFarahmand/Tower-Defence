using Lindon.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class StarterPage : UIPage
{
    GameStats stats;
    [SerializeField] private Button startButton;

    protected override void SetValues()
    {
        GameManager.Instance.Stats.ChangeState(GameStats.State.Home);
    }

    protected override void SetValuesOnSceneLoad()
    {
        stats = GameManager.Instance.Stats;

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() =>
        {
            stats.ChangeState(GameStats.State.Play);
        });
    }
}
