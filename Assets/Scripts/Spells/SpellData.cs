using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]
public class SpellData : ScriptableObject
{
    // TODO : catsing time, delay, Description
    public SpellName spellName;
    public float CoolDown;
    public int ManaCost;
    public Side affectedSide;
    public float baseRange;
    public bool modifiableRange;
    public bool targeted;
    
    [Header("Effects")]
    public List<Effect> Effects;
    public List<Effect> EffectsOnCaster;
    public PushEffect PushEffect;

    [Header("Interface")]
    public Sprite spellIcon;
    public String description;
    
    public float _cooldownTimer { get; private set; } = 0;
    
    public Entity Caster { get; private set; }
    public float ActualRange { get; private set; }

    private void OnEnable()
    {
        _cooldownTimer = 0;
    }

    public void UpdateParameters(Entity caster)
    {
        Caster = caster;
        if (modifiableRange)
        {
            ActualRange = baseRange + Caster.RangePoints;
        }
        else
        {
            ActualRange = baseRange;
        }
        
    }
    
    public void UpdateCooldown(Entity caster, float attackSpeed)
    {
        CoolDown = 1 / attackSpeed;
    }

    public void UpdateTimer()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    public void ResetTimer()
    {
        _cooldownTimer = CoolDown;
    }

    public float GetCoolDownTimerPercent()
    {
        if (_cooldownTimer <= 0f)
        {
            return 0f;
        }

        return _cooldownTimer / CoolDown;
    }

    public bool IsReady()
    {
        return _cooldownTimer <= 0f;
    }

    public bool IsCooldownUnderOne()
    {
        return _cooldownTimer <= 1f;
    }

    public bool CanSpellAffect(Entity target)
    {
        return affectedSide == Side.Both || target.side == affectedSide;
    }
}