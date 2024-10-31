using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Push Effect", menuName = "Effect/Push Effect")]
public class PushEffect : DurableEffect
{
    [SerializeField] private PushType _pushType;
    [SerializeField] private float _distance;

    public float PUSH_SPEED { get; private set; } = 6f;
    private Vector3 _direction;
    
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
    }
    
    public override void Apply(Entity target)
    {
        if (target.CannotBeMoved())
        {
            return;
        }
        
        DurationTimer = Mathf.Abs(_distance) / 8f;
        PUSH_SPEED = 8f * Mathf.Sign(_distance);

        var push = new PushEffectObject(_direction, PUSH_SPEED, DurationTimer, cleanAble, particleSystem, Caster);
        push.Apply(target);
    }
}

public enum PushType
{
    PushAway,
    PushAside,
}
