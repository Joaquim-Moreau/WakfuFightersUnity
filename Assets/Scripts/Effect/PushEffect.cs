using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Push Effect", menuName = "Effect/Push Effect")]
public class PushEffect : DurableEffect
{
    [SerializeField] private PushType _pushType;
    [SerializeField] private float _distance;

    public float PUSH_SPEED { get; private set; } = 6f;
    private int BASE_PUSH_DMG = 25;
    private Vector3 _direction;
    //private Entity _target;
    
    public void ChooseDirection(Holder holder, Entity target)
    {
        switch (_pushType)
        {
            case PushType.PushAway:
                if (holder.Direction == new Vector3(0, 0, 0))
                {
                    _direction = Statics.GetDirection(holder, target);
                }
                else
                {
                    _direction = holder.Direction;
                }
                break;
            
            case PushType.PushAside:
                Vector3 casterTargetVector = Statics.GetDirection(Caster, target);
                Vector3 casterHolderVector;
                if (holder.Direction == new Vector3(0, 0, 0))
                {
                    casterHolderVector = -Statics.GetDirection(holder, Caster);
                }
                else
                {
                    casterHolderVector = holder.Direction;
                }
                
                float angle = Statics.Angle(casterTargetVector, casterHolderVector);
                if (angle > 0f)
                {
                    _direction = new Vector3(casterHolderVector.y, -casterHolderVector.x, 0);
                }
                else
                {
                    _direction = new Vector3(-casterHolderVector.y, casterHolderVector.x, 0);
                }
                break;
            
            default:
                _direction = new Vector3(0, 0, 0);
                break;
        }
    }
    
    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
        PUSH_SPEED = 8f;
        DurationTimer = _distance / PUSH_SPEED;
    }
    
    public override void Apply(Entity target)
    {
        if (target.CannotBeMoved())
        {
            //Debug.Log("Can't move target");
            return;
        }
        ApplyParticle(target);
        target.currentPush = this;
    }
    
    public void UpdateMovement(Entity target)
    {
        target.transform.position += PUSH_SPEED * Time.deltaTime * _direction;
    }

    public void ApplyPushDamage(Entity target)
    {
        // TODO : apply stun for 0.5s
        int pushDmg = BASE_PUSH_DMG + Caster.GetPushDmg();
        target.TakeDmg(pushDmg, DmgType.True);
        Remove(target);
    }

    public void ApplySecondaryPushDamage(Entity target)
    {
        // TODO : apply stun for 0.5s
        if (Caster.side == target.side) return;
        target.TakeDmg(BASE_PUSH_DMG, DmgType.True);
    }
    
    public override void Remove(Entity target)
    {
        RemoveParticle();
    }
}

public enum PushType
{
    PushAway,
    PushAside,
}
