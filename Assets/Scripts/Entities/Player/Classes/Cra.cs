using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cra : Player
{
    private float _boostA = 0f;

    protected override void Update()
    {
        base.Update();
        if (_boostA > 0f)
        {
            _boostA -= Time.deltaTime;
        }
    }
    
    public override void CastSpace(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        spellManager.CastOnSelf(spellBook.SpaceSpell[0]);
    }
    
    public override void CastA(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (_boostA <= 0f)
        {
            spellManager.CastSpell(spellBook.ASpell[0], GetMousePos());
        }
        else
        {
            spellManager.CastSpell(spellBook.ASpell[1], GetMousePos());
        }
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
        spellManager.CastSpell(spellBook.RSpell[0], GetMousePos());
    }
    
    public override void HandleSpellLaunch(SpellName spellName)
    {
        switch (spellName)
        {
            case SpellName.TirsPuissants:
                break;
            case SpellName.FlecheAssaillante or SpellName.FlecheAssaillante2:
                foreach (var spell in spellBook.ASpell)
                {
                    spell.ResetTimer();
                }
                _boostA = 10f;
                _Animator.SetTrigger("Attack");
                break;
            default:
                _Animator.SetTrigger("Attack");
                break;
        }
    }
}
