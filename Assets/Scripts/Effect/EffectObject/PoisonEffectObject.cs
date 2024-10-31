using UnityEngine;

public class PoisonEffectObject : DurableEffectObject
{
    public float refresh = 0.5f;
    
    private DmgType _type;
    private float _damagePerTick;
    private float _baseLifeSteal;
    
    public PoisonEffectObject(float damagePerTick, DmgType type, float baseLifeSteal, float duration, bool cleanable,
        ParticleSystem particle, Entity caster) : base(duration, cleanable, particle, caster)
    {
        _damagePerTick = damagePerTick;
        _type = type;
        _baseLifeSteal = baseLifeSteal;
    }
    
    public void ApplyTick(Entity target)
    {
        float actualDmg = target.TakeDmg(_damagePerTick, _type);
        float stolenHp = actualDmg * (_baseLifeSteal + Caster.GetLifeSteal()) / 100f;
        Caster.HealHp(stolenHp);
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
