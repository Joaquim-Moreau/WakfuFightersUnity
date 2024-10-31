using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Special Effect", menuName = "Effect/Special Effect")]
public class SpecialEffect : DurableEffect
{
    [SerializeField] public Special SpecialType;
    
    public override void Apply(Entity target)
    {
        var special = new SpecialEffectObject(SpecialType, Duration, cleanAble, particleSystem, Caster);
        special.Apply(target);
    }
}
