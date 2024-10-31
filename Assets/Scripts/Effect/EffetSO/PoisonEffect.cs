using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Poison Effect", menuName = "Effect/Poison Effect")]
public class PoisonEffect : DurableEffect
{
    [SerializeField] private int _baseDmg;
    [SerializeField] float _phyRatio;
    [SerializeField] private float _magRatio;
    [SerializeField] private float _baseLifeSteal;
    [SerializeField] private DmgType _type;
    
    public override void Apply(Entity target)
    {
        if (target.CannotTakeDamage())
        {
            return;
        }

        if (Caster.CannotDealDamage())
        {
            return;
        }
        
        float dmg = _baseDmg + (_phyRatio * Mathf.Max(0, Caster.GetPhyDmg())) + (_magRatio * Mathf.Max(0, Caster.GetMagDmg()));
        float damagePerTick = dmg * (100f + Caster.GetDmgMultiplier())/100f;
        damagePerTick = 0.5f * damagePerTick / Duration;
        
        var poison = new PoisonEffectObject(damagePerTick, _type, _baseLifeSteal, Duration, cleanAble, particleSystem, Caster);
        poison.Apply(target);
        target.currentPoisons.Add(poison);
    }
}
