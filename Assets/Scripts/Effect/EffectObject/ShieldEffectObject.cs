using UnityEngine;

public class ShieldEffectObject : DurableEffectObject
{
    public int totalShield;
    
    public ShieldEffectObject(int shieldAmount, float duration, bool cleanable, ParticleSystem particle, 
        Entity caster) : base(duration, cleanable, particle, caster)
    {
        totalShield = shieldAmount;
    }

    public override void Apply(Entity target)
    {
        ApplyParticle(target);
    }

    public override void Remove(Entity target)
    {
        RemoveParticle();
    }
}
