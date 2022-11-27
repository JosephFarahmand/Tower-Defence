using Lindon.UI;
using UnityEngine;
using UnityEngine.UI;

public class LosePage : UIPage
{
    [SerializeField] private Button playAgainButton;

    protected override void SetValues()
    {

    }

    protected override void SetValuesOnSceneLoad()
    {
        playAgainButton.onClick.RemoveAllListeners();
        playAgainButton.onClick.AddListener(() =>
        {
            UserInterfaceManager.Open<GamePage>();
        });
    }
}
