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
    //public float duration;
    [NonSerialized] public float refresh;

    private float _damagePerTick;

    public override void Prepare(Entity caster, Entity target)
    {
        base.Prepare(caster, target);
        float dmg = _baseDmg + (_phyRatio * Mathf.Max(0, Caster.GetPhyDmg())) + (_magRatio * Mathf.Max(0, Caster.GetMagDmg()));
        _damagePerTick = dmg * (100f + Caster.GetDmgMultiplier())/100f;
        _damagePerTick = 0.5f * _damagePerTick / Duration;
    }

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
        ApplyParticle(target);
        target.currentPoisons.Add(this);
    }

    public override void Remove(Entity target)
    {
        RemoveParticle();
    }

    public void ApplyTick(Entity target)
    {
        float actualDmg = target.TakeDmg(_damagePerTick, _type);
        // LifeSteal
        float stolenHp = actualDmg * (_baseLifeSteal + Caster.GetLifeSteal()) / 100f;
        Caster.HealHp(stolenHp);
    }
}
