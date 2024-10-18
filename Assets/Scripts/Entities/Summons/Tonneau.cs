using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tonneau : Entity
{
    void OnEnable()
    {
        hp = maxHP;
    }

    public override float TakeDmg(float damage, DmgType type)
    {
        if (GetAutoAttack().IsReady())
        {
            spellManager.CastOnSelf(spellBook.AutoAttack);
        }

        return base.TakeDmg(damage, type);
    }
}
