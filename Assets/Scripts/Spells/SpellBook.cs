using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class SpellBook : MonoBehaviour
{
    [Header("Spells data")]
    public SpellData AutoAttack;
    public List<SpellData> SpaceSpell;
    public List<SpellData> ASpell;
    public List<SpellData> ZSpell;
    public List<SpellData> ESpell;
    public List<SpellData> RSpell;
    
    protected virtual void Update()
    {
        UpdateSpellData();
    }

    protected void UpdateSpellData()
    {
        foreach (var spellData in SpaceSpell)
        {
            spellData.UpdateTimer();
        }
        
        foreach (var spellData in ASpell)
        {
            spellData.UpdateTimer();
        }
        
        foreach (var spellData in ZSpell)
        {
            spellData.UpdateTimer();
        }
        
        foreach (var spellData in ESpell)
        {
            spellData.UpdateTimer();
        }
        
        foreach (var spellData in RSpell)
        {
            spellData.UpdateTimer();
        }
        
        AutoAttack?.UpdateTimer();
    }

    public void ResetCoolDowns()
    {
        foreach (var spellData in SpaceSpell)
        {
            spellData.NegateCoolDown();
        }
        
        foreach (var spellData in ASpell)
        {
            spellData.NegateCoolDown();
        }
        
        foreach (var spellData in ZSpell)
        {
            spellData.NegateCoolDown();
        }
        
        foreach (var spellData in ESpell)
        {
            spellData.NegateCoolDown();
        }
        
        foreach (var spellData in RSpell)
        {
            spellData.NegateCoolDown();
        }
    }
}
