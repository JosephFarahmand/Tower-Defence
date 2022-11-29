using Lindon.UI;
using UnityEngine;
using UnityEngine.UI;

public class WinPage : UIPage
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
            GameManager.Instance.ResetGame();
        });
    }
}
