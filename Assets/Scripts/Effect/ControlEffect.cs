using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Control Effect", menuName = "Effect/Control Effect")]
public class ControlEffect : DurableEffect
{ 
    [SerializeField] private Control controlType;
    private float _durationModifier = 1f;
    
    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
        _durationModifier = 1 + (caster.GetTenacity() / 100f) - (target.GetTenacity() / 100f);
        _durationModifier = 0.5f * Math.Clamp(_durationModifier, 0f, 2f);
        // TODO : revoir la formule
        DurationTimer = Duration * _durationModifier;
    }
    
    public override void Apply(Entity target)
    {
        if (target.CannotBeControlled())
        {
            //Debug.Log("Target cannot be controlled");
            return;
        }
        ManageControl(target, 1);
        ApplyParticle(target);
        target.currentEffects.Add(this);
    }

    public override void Remove(Entity target)
    {
        ManageControl(target, -1);
        RemoveParticle();
    }

    private void ManageControl(Entity target, int value)
    {
        switch (controlType)
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
