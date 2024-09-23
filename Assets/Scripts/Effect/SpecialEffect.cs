using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Special Effect", menuName = "Effect/Special Effect")]
public class SpecialEffect : DurableEffect
{
    [SerializeField] public Special SpecialType;

    public override void Prepare(Entity caster, Entity target)
    {
        base.Prepare(caster, target);
    }
    
    public override void Apply(Entity target)
    {
        ManageSpecial(target, 1);
        ApplyParticle(target);
        target.currentEffects.Add(this);
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
                Color tmp = target.GetComponent<SpriteRenderer>().color;
                tmp.a = (value == 1)? 0.5f: 255f;
                target.GetComponent<SpriteRenderer>().color = tmp;
                break;
        }
    }
}
