using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New heal Effect", menuName = "Effect/Cleanse Effect")]
public class CleanseEffect : Effect
{
    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
    }
    public override void Apply(Entity target)
    {
        if (target.currentEffects.Any())
        {
            foreach (var effect in target.currentEffects)
            {
                effect.Cleanse();
            }
        }
        
        if (target.currentPoisons.Any())
        {
            foreach (var effect in target.currentPoisons)
            {
                effect.Cleanse();
            }
        }
        
        if (target.currentShields.Any())
        {
            foreach (var effect in target.currentShields)
            {
                effect.Cleanse();
            }
        }
    }
}
