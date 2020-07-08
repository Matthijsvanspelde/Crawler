using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTriggerer : MonoBehaviour
{
    [SerializeField] private string combatTrigger = "";
    [SerializeField] private string damagedTrigger = "";
    [SerializeField] private string shootingTrigger = "";
    [SerializeField] private string walkingTrigger = "";
    [SerializeField] private string idleTrigger = "";
    [SerializeField] private string deathTrigger = "";

    private Animator animator;

    private void Awake() { animator = GetComponent<Animator>(); }

    public void SetIdleBool(bool value) { animator.SetBool(idleTrigger, value); }
    public void SetDeathBool(bool value) { animator.SetBool(deathTrigger, value); }
    public void SetShootingBool(bool value) { animator.SetBool(shootingTrigger, value); }
    public void SetWalkingBool(bool value) { animator.SetBool(walkingTrigger, value); }
    public void SetCombatBool(bool value) { animator.SetBool(combatTrigger, value); }
    public void SetDamagedBool(bool value) { animator.SetBool(damagedTrigger, value); }
    public void SetIdleTrigger() { animator.SetTrigger(idleTrigger); }
    public void SetDeathTrigger() { animator.SetTrigger(deathTrigger); }
    public void SetShootingTrigger() { animator.SetTrigger(shootingTrigger); }
    public void SetWalkingTrigger() { animator.SetTrigger(walkingTrigger); }
    public void SetCombatTrigger() { animator.SetTrigger(combatTrigger); }
    public void SetDamagedTrigger() { animator.SetTrigger(damagedTrigger); }
}
