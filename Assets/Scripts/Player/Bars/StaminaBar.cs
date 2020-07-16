using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        //slider setup
        slider = GetComponent<Slider>();
        slider.maxValue = PlayerStats.instance.MaxStamina.GetValue();
        slider.value = slider.maxValue;

        //Subscribe to method
        PlayerStats.instance.OnStaminaChanged += StaminaChanged;
    }

    private void StaminaChanged(int currentValue)
    {
        slider.value = currentValue;
    }
}
