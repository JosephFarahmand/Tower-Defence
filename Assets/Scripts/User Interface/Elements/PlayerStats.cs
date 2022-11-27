using Lindon.UI;
using TMPro;
using UnityEngine;

public class PlayerStats : UIElement
{
    [SerializeField] private TMP_Text goldAmountText;
    [SerializeField] private TMP_Text scoreAmountText;

    protected override void SetValues()
    {
        goldAmountText.SetText(GameManager.Instance.ProfileController.Profile.GoldAmount.ToString());
        scoreAmountText.SetText(GameManager.Instance.ProfileController.Profile.Score.ToString());

        GameManager.Instance.ProfileController.onChangePropertyCallback += updateUI;
    }

    private void updateUI(Profile profile)
    {
        goldAmountText.SetText(profile.GoldAmount.ToString());
        scoreAmountText.SetText(profile.Score.ToString());
    }
}
