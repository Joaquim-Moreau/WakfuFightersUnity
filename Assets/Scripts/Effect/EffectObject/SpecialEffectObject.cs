using UnityEngine;

public class SpecialEffectObject : DurableEffectObject
{
    public Special SpecialType;
    
    public SpecialEffectObject(Special specialType, float duration, bool cleanable, ParticleSystem particle, 
        Entity caster) : base(duration, cleanable, particle, caster)
    {
        SpecialType = specialType;
    }
    
    public override void Apply(Entity target)
    {
        target.currentEffects.Add(this);
        ManageSpecial(target, 1);
        ApplyParticle(target);
    }
    
    public override void Remove(Entity target)
    {
        ManageSpecial(target, -1);
        RemoveParticle();
    }
    
    private void ManageSpecial(Entity target, int value)
    {
        switch (SpecialType)
        {
            case Special.Invincible:
                target.invincibility += value;
                break;
            case Special.Gravitation:
                target.grounded += value;
                break;
            case Special.UnHeal:
                target.unHealAble += value;
                break;
            case Special.Pacifist:
                target.pacifist += value;
                break;
            case Special.Immovable:
                target.immovable += value;
                break;
            case Special.ControlImmune:
                target.controlImmune += value;
                break;
            case Special.Invisible:
                target.Invisible += value;
                break;
        }
    }
}
