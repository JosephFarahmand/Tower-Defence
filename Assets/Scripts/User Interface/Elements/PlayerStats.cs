using Lindon.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : UIElement
{
    [SerializeField] private TMP_Text goldAmountText;
    [SerializeField] private TMP_Text scoreAmountText;

    protected override void SetValues()
    {
        GameManager.Instance.ProfileController.onChangePropertyCallback += updateUI;
    }

    private void updateUI(Profile profile)
    {
        goldAmountText.SetText(profile.GoldAmount.ToString());
        scoreAmountText.SetText(profile.Score.ToString());
    }
}
