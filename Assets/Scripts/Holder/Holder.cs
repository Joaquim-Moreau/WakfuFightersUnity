using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Holder : Spell
{
    protected List<Entity> AlreadyHit = new List<Entity>();
    public Vector3 Direction { get; protected set; } = new Vector3(0, 0, 0);

    public override void Init(Entity caster, Vector3 launchPosition)
    {
        AlreadyHit = new List<Entity>();
    }
    
    public override void Init(Entity caster, Entity target)
    {
        AlreadyHit = new List<Entity>();
    }
    
    protected void ApplyEffects(Entity target)
    {
        foreach (var effect in spellData.Effects)
        {
            effect.Add(spellData.Caster, target);
        }
        
        if (spellData.PushEffect)
        {
            spellData.PushEffect.ChooseDirection(this, target);
            spellData.PushEffect.Add(spellData.Caster, target);
        }

        if (target != spellData.Caster)
        {
            foreach (var selfEffect in spellData.EffectsOnCaster)
            {
                selfEffect.Add(spellData.Caster, spellData.Caster);
            }
        }
    }

    protected void InitDirection(Entity caster)
    {
        Vector3 displacement = transform.position - caster.transform.position;
        displacement.z = 0f;
        float dist = Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y);
        
        Direction = displacement / dist;
    }
    
    protected void RotateObject()
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    protected void AddToHit(Entity target)
    {
        AlreadyHit.Add(target);
    }
}
