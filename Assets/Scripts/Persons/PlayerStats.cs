using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : StatLine
{
    #region SingleTon

    public static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    [Header("Mana and stamina")]
    [SerializeField] private Stat maxMana;
    [SerializeField] private Stat maxStamina;
    [Header("Tickrate")]
    [SerializeField] private Stat StaminaRegenTickRate;
    [SerializeField] private Stat ManaRegenTickRate;
    [SerializeField] private Stat StaminaRegenAmount;
    [SerializeField] private Stat ManaRegenAmount;

    #region Delegates
    
    public delegate void ManaChanged(int currentValue);
    public ManaChanged OnManaChanged;

    public delegate void StaminaChanged(int currentValue);
    public ManaChanged OnStaminaChanged;

    #endregion

    private float currentMana = 0;
    private float currentStamina = 0;

    private float currentManaTickTime = 0;
    private float currentStaminaTickTime = 0;

    //Set currentvalues to start values
    private void Start()
    {
        currentMana = Mathf.RoundToInt(maxMana.GetValue());
        currentStamina = Mathf.RoundToInt(MaxStamina.GetValue());
        currentHealth = Mathf.RoundToInt(MaxHealth.GetValue());
    }

    private void Update()
    {
        HandleManaTickTime();
        HandleStaminaTickTime();
    }

    private void HandleManaTickTime()
    {
        if (currentMana < maxMana.GetValue())
            currentManaTickTime += Time.deltaTime;
        else
            return;

        if (currentManaTickTime >= ManaRegenTickRate.GetValue())
        {
            currentManaTickTime = 0;
            IncreaseCurrentMana(ManaRegenAmount.GetValue());
        }
    }

    private void HandleStaminaTickTime()
    {
        if (currentStamina < maxStamina.GetValue())
            currentStaminaTickTime += Time.deltaTime;
        else
            return;

        if (currentStaminaTickTime >= StaminaRegenTickRate.GetValue())
        {
            currentStaminaTickTime = 0;
            IncreaseCurrentMana(StaminaRegenAmount.GetValue());
        }
    }

    private void InvokeManaChange()
    {
        OnManaChanged.Invoke(Mathf.RoundToInt(currentMana));
    }

    private void InvokeStaminaChange()
    {
        OnStaminaChanged.Invoke(Mathf.RoundToInt(currentStamina));
    }

    public void IncreaseCurrentStamina(float value) { currentStamina += value; InvokeStaminaChange(); }
   
    public void DecreaseCurrentStamina(float value) { currentStamina -= value; InvokeStaminaChange(); }

    public void IncreaseCurrentMana(float value) { currentMana += value; InvokeManaChange(); }

    public void DecreaseCurrentMana(float value) { currentMana -= value; InvokeManaChange(); }

    public Stat MaxMana { get => maxMana; }
    public Stat MaxStamina { get => maxStamina; }
    public float CurrentMana { get => currentMana; private set => currentMana = value; }
    public float CurrentStamina { get => currentStamina; private set => currentStamina = value; }
}
