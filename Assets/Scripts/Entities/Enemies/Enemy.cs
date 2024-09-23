using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    protected override void Die()
    {
        BattleSystem.numberOfKills += 1;
        BattleSystem.SpawnBoss();
        base.Die();
    }

    public override void HandleSpellLaunch(SpellName spellName)
    {
        if (spellName == GetAutoAttack().spellName)
        {
            _Animator.SetTrigger("Attack");
        }
    }
}
