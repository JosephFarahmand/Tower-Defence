using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualHealthBar : MonoBehaviour
{
    [SerializeField] private Image slider;

    public void SetValue(float amount)
    {
        slider.fillAmount = Mathf.Clamp01(amount);
    }
}
