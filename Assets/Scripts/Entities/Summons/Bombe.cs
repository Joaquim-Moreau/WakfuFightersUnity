using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombe : Summon
{
    public override float TakeDmg(float damage, DmgType type)
    {
        spellManager.CastOnSelf(spellBook.AutoAttack);
        
        hp -= 1;
        return 0f;
    }
}
