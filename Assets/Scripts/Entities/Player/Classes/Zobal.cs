using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Zobal : Player
{
    [SerializeField] private Image currentMaskImage;

    [SerializeField] private Sprite psychopath;
    [SerializeField] private Sprite audacious;
    [SerializeField] private Sprite coward;
    
    private enum Masque
    {
        Intrepide,
        Psycopathe,
        Pleutre
    }

    private Masque _currentMask = Masque.Intrepide;
    
    public override void CastSpace(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.SpaceSpell[0].spellName);
        spellManager.CastOnSelf(spellBook.SpaceSpell[0]);
    }
    
    public override void CastA(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (_currentMask == Masque.Psycopathe)
        {
            Debug.Log("Casting : " + spellBook.ASpell[1].spellName);
            spellManager.CastSpell(spellBook.ASpell[1], GetMousePos());
        }
        else
        {
            Debug.Log("Casting : " + spellBook.ASpell[0].spellName);
            spellManager.CastSpell(spellBook.ASpell[0], GetMousePos());
        }
    }
    
    public override void CastZ(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        if (_currentMask == Masque.Intrepide)
        {
            Debug.Log("Casting : " + spellBook.ZSpell[1].spellName);
            spellManager.CastOnSelf(spellBook.ZSpell[1]);
        }
        else
        {
            Debug.Log("Casting : " + spellBook.ZSpell[0].spellName);
            spellManager.CastOnSelf(spellBook.ZSpell[0]);
        }
    }
    
    public override void CastE(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        if (_currentMask == Masque.Pleutre)
        {
            Debug.Log("Casting : " + spellBook.ESpell[1].spellName);
            spellManager.CastSpell(spellBook.ESpell[1], GetMousePos());
        }
        else
        {
            Debug.Log("Casting : " + spellBook.ESpell[0].spellName);
            spellManager.CastSpell(spellBook.ESpell[0], GetMousePos());
        }
    }

    public override void CastR(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Casting : " + spellBook.RSpell[0].spellName);
        spellManager.CastSpell(spellBook.RSpell[0], GetMousePos());
    }
    
    public override void HandleSpellLaunch(SpellName spellName)
    {
        switch (spellName)
        {
            case SpellName.Mascarade:
                _Animator.SetTrigger("Dash");
                break;
            case SpellName.Ponteira or SpellName.PonteiraBoost:
                foreach (var spell in spellBook.ESpell)
                {
                    spell.ResetTimer();
                }
                _Animator.SetTrigger("Dash");
                break;
            case SpellName.Furia:
                foreach (var spell in spellBook.ASpell)
                {
                    spell.ResetTimer();
                }
                _Animator.SetTrigger("Spell");
                break;
            case SpellName.FuriaBoost:
                foreach (var spell in spellBook.ASpell)
                {
                    spell.ResetTimer();
                }
                _Animator.SetTrigger("DashAttack");
                break;
            case SpellName.ZobalAutoAttack:
                _Animator.SetTrigger("Attack");
                break;
            case SpellName.Transe or SpellName.TranseBoost:
                foreach (var spell in spellBook.ZSpell)
                {
                    spell.ResetTimer();
                }
                break;
            case SpellName.Masque:
                if (_currentMask == Masque.Intrepide)
                {
                    _currentMask = Masque.Psycopathe;
                    currentMaskImage.sprite = psychopath;
                }
                else
                {
                    if (_currentMask == Masque.Psycopathe)
                    {
                        _currentMask = Masque.Pleutre;
                        currentMaskImage.sprite = coward;
                    }
                    else
                    {
                        _currentMask = Masque.Intrepide;
                        currentMaskImage.sprite = audacious;
                    }
                }
                break;
        }
    }
}
