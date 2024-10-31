using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Eliatrope : Player
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
            case SpellName.EliaAutoAttack or SpellName.EtoilesJumelles or SpellName.RayonDeWakfu:
                _Animator.SetTrigger("Spell");
                break;
            case SpellName.LameDeWakfu:
                _Animator.SetTrigger("Attack");
                break;
            default:
                break;
        }
    }
}
