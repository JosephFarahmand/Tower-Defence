using Lindon.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterPage : UIPage
{
    [SerializeField] private Button startButton;

    protected override void SetValues()
    {

    }

    protected override void SetValuesOnSceneLoad()
    {
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() =>
        {
            UserInterfaceManager.Open<GamePage>();
        });
    }
}
