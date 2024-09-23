using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Shield Effect", menuName = "Effect/Shield Effect")]
public class ShieldEffect : DurableEffect
{
    [SerializeField] private int _baseShield;
    [SerializeField] private float _phyRatio;
    [SerializeField] private float _magRatio;

    [NonSerialized] public int totalShield;

    public override void Prepare(Entity caster, Entity target)
    {
        base.Prepare(caster, target);
        float shield = _baseShield + (_phyRatio * Mathf.Max(0, caster.GetPhyDmg())) + (_magRatio * Mathf.Max(0, caster.GetMagDmg()));
        totalShield = (int)shield;
    }
    
    public override void Apply(Entity target)
    {
        ApplyParticle(target);
        target.currentShields.Add(this);
    }
    
    public override void Remove(Entity target)
    {
        RemoveParticle();
    }
}

