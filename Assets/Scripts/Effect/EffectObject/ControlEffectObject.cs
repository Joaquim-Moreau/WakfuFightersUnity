using UnityEngine;

public class ControlEffectObject : DurableEffectObject
{
    private Control _controlType;
    
    public ControlEffectObject(Control controlType, float duration, bool cleanable, ParticleSystem particle, 
        Entity caster) : base(duration, cleanable, particle, caster)
    {
        _controlType = controlType;
    }
    
    public override void Apply(Entity target)
    {
        target.currentEffects.Add(this);
        ManageControl(target, 1);
        ApplyParticle(target);
    }

    public override void Remove(Entity target)
    {
        ManageControl(target, -1);
        RemoveParticle();
    }
    
    private void ManageControl(Entity target, int value)
    {
        switch (_controlType)
        {
            case Control.Stun:
                target.stun += value;
                break;
            case Control.Silence:
                target.silence += value;
                break;
            case Control.Disarm:
                target.disarm += value;
                break;
            case Control.Exhausted:
                target.exhaust += value;
                break;
            case Control.Root:
                target.root += value;
                break;
        }
    }
}
