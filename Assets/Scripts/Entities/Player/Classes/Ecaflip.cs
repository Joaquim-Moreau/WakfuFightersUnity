using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ecaflip : Player
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
    }
    
    public override void CastE(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastSpell(spellBook.ESpell[0], GetMousePos());
    }
    
    public override void CastR(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        int randomChoice = Random.Range(0, 3);
        spellManager.CastSpell(spellBook.RSpell[randomChoice], GetMousePos());
    }
    
    public override void HandleSpellLaunch(SpellName spellName)
    {
        switch (spellName)
        {
            case SpellName.EcaAutoAttack:
                _Animator.SetTrigger("Attack");
                break;
            case SpellName.Rekop or SpellName.Rekop2 or SpellName.Rekop3:
                foreach (var rekop in spellBook.RSpell)
                {
                    rekop.ResetTimer();
                }
                _Animator.SetTrigger("Spell");
                break;
            case SpellName.BondDuFelin:
                _Animator.SetTrigger("Dash");
                break;
            default:
                _Animator.SetTrigger("Spell");
                break;
        }
    }
}
