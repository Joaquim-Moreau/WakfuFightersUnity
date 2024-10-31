using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Control Effect", menuName = "Effect/Control Effect")]
public class ControlEffect : DurableEffect
{ 
    [SerializeField] private Control controlType;
    private float _durationModifier = 1f;
    
    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
    }
    
    public override void Apply(Entity target)
    {
        if (target.CannotBeControlled())
        {
            return;
        }
        _durationModifier = 1 + (Caster.GetTenacity() / 100f) - (target.GetTenacity() / 100f);
        _durationModifier = 0.5f * Math.Clamp(_durationModifier, 0f, 2f);
        DurationTimer = Duration * _durationModifier;

        var control = new ControlEffectObject(controlType, DurationTimer, cleanAble, particleSystem, Caster);
        control.Apply(target);
    }
}
