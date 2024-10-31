using UnityEngine;


public class PushEffectObject : DurableEffectObject
{
    private float PUSH_SPEED;
    private Vector3 _direction;
    
    public PushEffectObject(Vector3 direction, float speed, float duration, bool cleanable, ParticleSystem particle, 
        Entity caster) : base(duration, cleanable, particle, caster)
    {
        _direction = direction;
        PUSH_SPEED = speed;
    }
    
    public void UpdateMovement(Entity target)
    {
        target.transform.position += PUSH_SPEED * Time.deltaTime * _direction;
    }
    
    public override void Apply(Entity target)
    {
        target.currentPush = this;
        ApplyParticle(target);
    }

    public override void Remove(Entity target)
    {
        RemoveParticle();
    }
}
