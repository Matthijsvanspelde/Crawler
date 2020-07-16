using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        //slider setup
        slider = GetComponent<Slider>();
        slider.maxValue = PlayerStats.instance.MaxHealth.GetValue();
        slider.value = slider.maxValue;

        //Subscribe to method
        PlayerStats.instance.OnHealthChanged += HealthChanged;
    }

    private void HealthChanged(int currentValue)
    {
        slider.value = currentValue;
    }
}
