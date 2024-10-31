using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sram : Player
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
        spellManager.CastSpell(spellBook.ZSpell[0], GetMousePos());
        
        
        spellManager.CastSpell(spellBook.ZSpell[0], GetMousePos());
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
            case SpellName.CoupeGorge or SpellName.SramAutoAttack:
                _Animator.SetTrigger("Attack");
                break;
            case SpellName.Invisibilite:
                break;
            case SpellName.MiseAMort:
                _Animator.SetTrigger("Ult");
                break;
            default:
                _Animator.SetTrigger("Spell");
                break;
        }
    }
}
