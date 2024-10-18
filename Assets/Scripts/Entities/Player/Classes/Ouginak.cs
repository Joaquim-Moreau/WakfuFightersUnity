using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ouginak : Player
{
    public override void CastSpace(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.SpaceSpell[0].spellName);
        spellManager.CastSpell(spellBook.SpaceSpell[0], GetMousePos());
    }
    
    public override void CastA(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.ASpell[0].spellName);
        spellManager.CastSpell(spellBook.ASpell[0], GetMousePos());
    }
    
    public override void CastZ(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.ZSpell[0].spellName);
        spellManager.CastOnSelf(spellBook.ZSpell[0]);
    }
    
    public override void CastE(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.ESpell[0].spellName);
        spellManager.CastOnSelf(spellBook.ESpell[0]);
    }
    
    public override void CastR(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.RSpell[0].spellName);
        spellManager.CastOnSelf(spellBook.RSpell[0]);
    }
    
    public override void HandleSpellLaunch(SpellName spellName)
    {
        switch (spellName)
        {
            case SpellName.XelAutoAttack or SpellName.FrappeDuXelor or SpellName.Aiguille:
                _Animator.SetTrigger("Spell");
                break;
            default:
                _Animator.SetTrigger("Attack");
                break;
        }
    }
}
