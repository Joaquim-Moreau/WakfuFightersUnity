using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Osamodas : Player
{
    public override void CastSpace(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastSpell(spellBook.SpaceSpell[0], GetMousePos());
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
    }
    
    public override void CastE(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastOnSelf(spellBook.ESpell[0]);
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
            case SpellName.Fouet:
                _Animator.SetTrigger("Attack");
                break;
            case SpellName.SuperBonbon or SpellName.Invocation or SpellName.OsaAutoAttack:
                _Animator.SetTrigger("Spell");
                break;
            default:
                break;
        }
    }
}
