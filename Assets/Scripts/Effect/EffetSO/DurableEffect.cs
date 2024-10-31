using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DurableEffect : Effect
{
    [SerializeField] protected float Duration;
    [SerializeField] protected ParticleSystem particleSystem;
    
    protected float DurationTimer;

    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
        DurationTimer = Duration;
    }
}
