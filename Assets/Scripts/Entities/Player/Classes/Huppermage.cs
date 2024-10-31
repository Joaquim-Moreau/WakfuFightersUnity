using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Huppermage : Player
{
    public override void CastSpace(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastOnSelf(spellBook.SpaceSpell[0]);
    }
    
    public override void CastA(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastSpell(spellBook.ASpell[0], GetMousePos());
    }
    
    public override void CastZ(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastOnSelf(spellBook.ZSpell[0]);
    }
    
    public override void CastE(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastSpell(spellBook.ESpell[0], GetMousePos());
    }
    
    public override void CastR(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastSpell(spellBook.RSpell[0], GetMousePos());
    }

    public override void HandleSpellLaunch(SpellName spellName)
    {
        switch (spellName)
        {
            case SpellName.HupperAutoAttack:
                _Animator.SetTrigger("Attack");
                break;
            case SpellName.Meteore:
                _Animator.SetTrigger("Earth");
                break;
            case SpellName.Glacier:
                _Animator.SetTrigger("Water");
                break;
            case SpellName.Ouragan :
                _Animator.SetTrigger("Air");
                break;
            case SpellName.TraitArdent:
                _Animator.SetTrigger("Fire");
                break;
            default:
                break;
        }
    }
}
