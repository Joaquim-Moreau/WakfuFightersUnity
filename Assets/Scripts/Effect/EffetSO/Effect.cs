using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Effect : ScriptableObject
{
    protected Entity Caster = null;
    [SerializeField] protected Side AffectedSide;
    public bool cleanAble = true;

    public void Add(Entity caster, Entity target)
    {
        if (AffectedSide != target.side && AffectedSide != Side.Both) return;
        Prepare(caster, target);
        Apply(target);
    }
    
    public abstract void Apply(Entity target);
    

    public abstract void Prepare(Entity caster, Entity target);
}
