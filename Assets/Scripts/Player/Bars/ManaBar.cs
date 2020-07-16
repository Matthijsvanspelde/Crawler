using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        //slider setup
        slider = GetComponent<Slider>();
        slider.maxValue = PlayerStats.instance.MaxMana.GetValue();
        slider.value = slider.maxValue;

        //Subscribe to method
        PlayerStats.instance.OnManaChanged += ManaChanged;
    }

    private void ManaChanged(int currentValue)
    {
        slider.value = currentValue;
        slider.GraphicUpdateComplete();
    }
}
