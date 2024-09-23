using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Huppermage : Player
{
    private enum ElementalRune
    {
        Earth,
        Water,
        Air,
        Fire,
        Neutral
    }

    //private ElementalRune _currentRune = ElementalRune.Neutral;

    public override void CastSpace(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.SpaceSpell[0].spellName);
    }
    
    public override void CastA(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.ASpell[0].spellName);
    }
    
    public override void CastZ(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.ZSpell[0].spellName);
        
        
    }
    
    public override void CastE(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.ESpell[0].spellName);
    }
    
    public override void CastR(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.RSpell[0].spellName);
    }

    public override void HandleSpellLaunch(SpellName spellName)
    {
        switch (spellName)
        {
            case SpellName.SurchargeRunique:
                //_currentRune = ElementalRune.Neutral;
                break;
            case SpellName.Meteore or SpellName.LameDeRoc or SpellName.Seisme:
                //_currentRune = ElementalRune.Earth;
                break;
            case SpellName.Source or SpellName.Glacier or SpellName.Bulle:
                //_currentRune = ElementalRune.Water;
                break; 
            case SpellName.Bourrasque or SpellName.Traversee or SpellName.Ouragan:
                //_currentRune = ElementalRune.Air;
                break;
            case SpellName.TraitArdent or SpellName.Volcan or SpellName.Deflagration:
                //_currentRune = ElementalRune.Fire;
                break;
            default:
                break;
        }
    }
}
