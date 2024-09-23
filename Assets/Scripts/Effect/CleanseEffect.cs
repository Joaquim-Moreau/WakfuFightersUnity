using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

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
                if (effect.cleanAble)
                {
                    effect.Cleanse();
                }
            }
        }
        
        if (target.currentPoisons.Any())
        {
            foreach (var effect in target.currentPoisons)
            {
                if (effect.cleanAble)
                {
                    effect.Cleanse();
                }
            }
        }
        
        if (target.currentShields.Any())
        {
            foreach (var effect in target.currentShields)
            {
                if (effect.cleanAble)
                {
                    effect.Cleanse();
                }
            }
        }
    }
}
