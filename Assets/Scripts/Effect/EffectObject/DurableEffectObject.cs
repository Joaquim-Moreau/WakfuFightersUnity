using UnityEngine;

public class DurableEffectObject
{
    protected Entity Caster;
    protected float DurationTimer;
    protected ParticleSystem particleSystem;
    
    protected ParticleSystem instance = null;
    protected bool CleanAble;
    
    public DurableEffectObject(float duration, bool cleanable, ParticleSystem particle, Entity caster)
    {
        DurationTimer = duration;
        particleSystem = particle;
        CleanAble = cleanable;
        Caster = caster;
    }
    
    public void UpdateDuration()
    {
        DurationTimer -= Time.deltaTime;
    }
    
    public void Cleanse()
    {
        if (!CleanAble) return;
        DurationTimer = 0f;
    }
    
    public bool IsExpired()
    {
        return DurationTimer <= 0f;
    }
    
    public virtual void Apply(Entity target) {}
    public virtual void Remove(Entity target) {}
    
    protected void ApplyParticle(Entity target)
    {
        if (particleSystem)
        {
            instance = Object.Instantiate(particleSystem, 
                                              target.transform.position, 
                                              Quaternion.identity, 
                                              target.transform);

            if (target.side == Side.Green)
            {
                instance.gameObject.layer = 0;
                foreach (Transform child in instance.gameObject.transform)
                {
                    child.gameObject.layer = 0;
                }
            }
            else
            {
                instance.gameObject.layer = target.visibleToOpponent ? 0 : 7;
                foreach (Transform child in instance.gameObject.transform)
                {
                    child.gameObject.layer = target.visibleToOpponent ? 0 : 7;
                }
            }
            
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
        }
        else
        {
            instance = null;
        }
    }

    protected void RemoveParticle()
    {
        if (instance is null) return;
        
        instance.Stop();
        instance.Clear();
        Object.Destroy(instance.gameObject);
        instance = null;
    }
}
