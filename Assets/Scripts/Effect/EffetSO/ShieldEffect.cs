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
    
    public override void Apply(Entity target)
    {
        int totalShield = (int)(_baseShield + (_phyRatio * Mathf.Max(0, Caster.GetPhyDmg())) + (_magRatio * Mathf.Max(0, Caster.GetMagDmg())));
        var shield = new ShieldEffectObject(totalShield, Duration, cleanAble, particleSystem, Caster);
        shield.Apply(target);
        target.currentShields.Add(shield);
    }
}

