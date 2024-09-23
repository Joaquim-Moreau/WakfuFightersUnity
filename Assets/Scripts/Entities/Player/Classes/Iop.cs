using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Iop : Player
{
    private bool _punch = false;
    
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
        
        if (spellBook.ASpell[0].IsReady())
        {
            _punch = true;
        }
    }
    
    public override void CastZ(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.ZSpell[0].spellName);
        spellManager.CastSpell(spellBook.ZSpell[0], GetMousePos());
    }
    
    public override void CastE(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.ESpell[0].spellName);
        spellManager.CastSpell(spellBook.ESpell[0], GetMousePos());
    }

    public override void CastR(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.RSpell[0].spellName);
        spellManager.CastSpell(spellBook.RSpell[0], GetMousePos());
    }

    public override SpellData GetAutoAttack()
    {
        if (_punch)
        {
            return spellBook.ASpell[0];
        }

        return spellBook.AutoAttack;
    }
    
    public override void HandleSpellLaunch(SpellName spellName)
    {
        if (spellName == SpellName.Punch)
        {
            _punch = false;
        }

        switch (spellName)
        {
            case SpellName.Punch or SpellName.Duel or SpellName.EpeeCeleste:
                _Animator.SetTrigger("Punch");
                break;
            case SpellName.Colere:
                _Animator.SetTrigger("Ult");
                break;
            case SpellName.Bond:
                _Animator.SetTrigger("Dash");
                break;
            default:
                _Animator.SetTrigger("Attack");
                break;
        }
        
    }
}
