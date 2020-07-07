using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;

    private List<int> modifiers = new List<int>();

    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        modifiers.Remove(modifier);
    }

    public float GetValue()
    {
        float currentValue = baseValue;

        foreach (int modifier in modifiers)
        {
            currentValue += modifier;
        }

        currentValue = Mathf.Clamp(currentValue, 0, int.MaxValue);

        return currentValue;
    }
}
