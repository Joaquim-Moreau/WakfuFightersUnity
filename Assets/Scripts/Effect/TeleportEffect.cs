using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : Effect
{
    private Vector3 _arrival;
    
    private Vector3 _pivot;
    private bool _fromCaster;
    
    public void Initialize(Entity caster, float delay = 0f)
    {
        Caster = caster;

        if (caster.side == Side.Green)
        {
            AffectedSide = Side.Red;
        } else if (caster.side == Side.Red)
        {
            AffectedSide = Side.Green;
        }
        else
        {
            //Debug.Log("Error side entity");
        }
    }
    
    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
        Vector3 casterTargetVector = Statics.GetVector(Caster, target);
        _arrival = target.transform.position - 2 * casterTargetVector;
    }
    
    public override void Apply(Entity target)
    {
        if (target.CannotBeMoved())
        {
            Debug.Log("Can't move target");
            return;
        }
        // TODO : check if arrival is an ok position
        // if not, ray cast from caster until wall, _arrival = intersection
        target.transform.position = _arrival;
    }
}
